using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.Repositories;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.MarkAsRecived;

public class MarkAsRecivedHandler : IRequestHandler<MarkAsRecivedCommand, Result>
{
    private readonly IOrderRepository _orderRepository;

    public MarkAsRecivedHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(MarkAsRecivedCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result.Failed(OrderErrors.OrderNotFound(request.OrderId));

        var result = order.SetStatusAsReceivedPackage();

        if (!result.IsSuccess)
            return result;

        await _orderRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
