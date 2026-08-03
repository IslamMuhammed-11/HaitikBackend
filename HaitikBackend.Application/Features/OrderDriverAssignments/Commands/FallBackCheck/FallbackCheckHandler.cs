using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Commands.FallBackCheck;

public class FallbackCheckHandler : IRequestHandler<FallbackCheckCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public FallbackCheckHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(FallbackCheckCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdWithAssignmentsAsync(request.orderId, cancellationToken);

        if (order is null)
            return;

        // if already assigned, nothing to do
        if (order.AssignedDriver is not null)
        {
            var assignments = order.OrderDriverAssignments.Where(a => a.Status == enOrderDriverAssignmentStatus.Pending);

            await ExpirePendingAssignment(assignments, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return;
        }

        var recomndedDriver = request.drivers.Where(e => e.orders)

        var chosen = scored
            .OrderBy(s => s.distance)
            .ThenBy(s => s.activeOrders)
            .FirstOrDefault();

        if (chosen.userId == 0)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        // force assign
        var assignment = order.RequestToAssignDriver(chosen.userId);
        _unitOfWork.OrderDriverAssignments.Add(assignment);

        // assign driver to order
        order.AssignDriver(chosen.userId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }


    private async Task<int> GetActiveOrdersCountForDriver(int driverId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Orders.Query()
            .Where(o => o.AssignedDriver == driverId && o.Status != enOrderStatus.Delivered)
            .CountAsync(cancellationToken);
    }




    private async Task ExpirePendingAssignment(IEnumerable<Domain.Entities.OrderDriverAssignment> pendingAssignments, CancellationToken cancellationToken)
    {
        Parallel.ForEach(pendingAssignments, pa =>
        {
            new ParallelOptions
            {
                MaxDegreeOfParallelism = 20
            };


            pa.MarkAsExpired();
            _unitOfWork.OrderDriverAssignments.Update(pa);
        });
    }

}
