using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
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

        var result = order.UpdateLocation(request.NewLocation);

        if (!result.IsSuccess)
            return Result<ChangeLocationResponse>.Failed(result.Error!);


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new ChangeLocationResponse(order.Id, request.NewLocation, DateTime.UtcNow);

        return Result<ChangeLocationResponse>.Success(response);
    }
}
