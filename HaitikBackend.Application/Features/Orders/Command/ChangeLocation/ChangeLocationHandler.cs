using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.Repositories;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.ChangeLocation;

public class ChangeLocationHandler : IRequestHandler<ChangeLocationCommand, Result<ChangeLocationResponse>>
{
    private readonly IOrderRepository _orderRepository;

    public ChangeLocationHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<ChangeLocationResponse>> Handle(ChangeLocationCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result<ChangeLocationResponse>.Failed(OrderErrors.OrderNotFound(request.OrderId));

        var result = order.UpdateLocation(request.NewLocation);

        if (!result.IsSuccess)
            return Result<ChangeLocationResponse>.Failed(result.Error!);


        await _orderRepository.SaveChangesAsync(cancellationToken);

        var response = new ChangeLocationResponse(order.Id, request.NewLocation, DateTime.UtcNow);

        return Result<ChangeLocationResponse>.Success(response);
    }
}
