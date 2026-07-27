using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.Repositories;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.MarkAsDelivered;

public class MarkAsDeliveredHandler : IRequestHandler<MarkAsDeliveredCommand, Result>
{
    private readonly IOrderRepository _orderRepository;

    public MarkAsDeliveredHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(MarkAsDeliveredCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result.Failed(OrderErrors.OrderNotFound(request.OrderId));

        var result = order.SetStatusAsDelivered();

        if (!result.IsSuccess)
            return result;

        await _orderRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
