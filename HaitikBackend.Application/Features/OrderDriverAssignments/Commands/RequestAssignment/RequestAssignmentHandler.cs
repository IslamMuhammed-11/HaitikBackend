using HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Commands.RequestAssignment;

public class RequestAssignmentHandler : IRequestHandler<RequestAssignmentCommand, Result<DriverOffer>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RequestAssignmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DriverOffer>> Handle(RequestAssignmentCommand request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Drivers.GetByIdAsync(request.DriverId, cancellationToken);

        if (driver is null)
            return Result<DriverOffer>.Failed(DriverErrors.DriverNotFound(request.DriverId));

        var result = await CheckMaximumOrderPerDay(driver, cancellationToken);

        if (!result.IsSuccess)
            return Result<DriverOffer>.Failed(result.Error!);

        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result<DriverOffer>.Failed(OrderErrors.OrderNotFound(request.OrderId));

        var assignmentRequest = order.RequestToAssignDriver(driver.UserId);

        _unitOfWork.OrderDriverAssignments.Add(assignmentRequest);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var offer = new DriverOffer(order.Id, order.DeliveryLocation, assignmentRequest.CreatedAt, null);

        return Result<DriverOffer>.Success(offer);
    }




    private async Task<Result> CheckMaximumOrderPerDay(Driver driver, CancellationToken cancellationToken)
    {
        if (driver.MaximumOrdersPerDay is null)
            return Result.Success();

        var activeOrdersQuery = _unitOfWork.Orders.Query().Where(o => o.AssignedDriver == driver.UserId && o.Status != enOrderStatus.Delivered);

        var activeCount = await activeOrdersQuery.CountAsync(cancellationToken);

        if (activeCount >= driver.MaximumOrdersPerDay.Value)
            return Result<DriverOffer>.Failed(DriverErrors.DriverReachedMaximumOrdersPerDay(driver.UserId));

        return Result.Success();
    }

}
