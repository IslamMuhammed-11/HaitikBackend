
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.DomainServices.OrderAssignmentService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Commands.FallBackCheck;

public class FallbackCheckHandler : IRequestHandler<FallbackCheckCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderAssignmentService _orderAssignmentService;
    private readonly ILogger<FallbackCheckHandler> _logger;
    public FallbackCheckHandler(IUnitOfWork unitOfWork, IOrderAssignmentService orderAssignmentService, ILogger<FallbackCheckHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _orderAssignmentService = orderAssignmentService;
        _logger = logger;
    }

    public async Task Handle(FallbackCheckCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdWithAssignmentsAsync(request.orderId, cancellationToken);

        if (order is null)
            return;
        // if already assigned, nothing to do
        if (order.AssignedDriver is not null)
        {

            order.ExpirePendingAssignments();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return;
        }

        var recommendedDriver = request.drivers.MinBy(d => d.ActiveOrdersCount);

        if (recommendedDriver is null)
        {
            _logger.LogWarning("No Driver Was Sent to the FallbackCheckHandler");
            return;
        }

        var assignresult =  _orderAssignmentService.AcceptOrderAssignment
                                                        (order, order.OrderDriverAssignments.First(a => a.DriverId == recommendedDriver.DriverId), cancellationToken);

        if (!assignresult.IsSuccess)
        {
            _logger.LogInformation("Assignment was handled");
            return;
        }

        order.ExpirePendingAssignments();


        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }




}
