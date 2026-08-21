using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.ValueObjects;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.ChangeLocation;

public class ChangeLocationHandler : IRequestHandler<ChangeLocationCommand, Result<ChangeLocationResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangeLocationHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ChangeLocationResponse>> Handle(ChangeLocationCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result<ChangeLocationResponse>.Failed(OrderErrors.OrderNotFound(request.OrderId));

        var newLocation = GeoLocation.Create(request.Latitiude, request.Longitude);

        var result = order.UpdateLocation(newLocation);

        if (!result.IsSuccess)
            return Result<ChangeLocationResponse>.Failed(result.Error!);


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new ChangeLocationResponse(order.Id, request.Latitiude, request.Longitude, DateTime.UtcNow);

        return Result<ChangeLocationResponse>.Success(response);
    }
}
