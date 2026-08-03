using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.UnitOfWork;

namespace HaitikBackend.Application.Common.Interfaces.OrderAssignment;

public interface IOrderAssignmentService
{
    Task<Result> AcceptOrderAssignment(Order order, OrderDriverAssignment assignment,CancellationToken cancellationToken = default);

}
