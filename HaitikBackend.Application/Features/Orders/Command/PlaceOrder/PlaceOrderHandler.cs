using HaitikBackend.Application.Interfaces.PhoneNumberChecker;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.DomainEvents.OrderEvents;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.PlaceOrder;

public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhoneNumberChecker _phoneNumberChecker;
    public PlaceOrderHandler(IUnitOfWork unitOfWork, IPhoneNumberChecker phoneNumberChecker)
    {
        _unitOfWork = unitOfWork;
        _phoneNumberChecker = phoneNumberChecker;
    }

    public async Task<Result<int>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {

        var validatorResult = await ValidateRequest(request, cancellationToken);

        if (!validatorResult.IsSuccess)
            return Result<int>.Failed(validatorResult.Error!);


        //Will Later Calculate the GeoZone when Handeled.
        var order = Order.Create(request.CustomerPhoneNumber, DateTime.UtcNow, request.PickupLocation, null, request.employeeId);

        _unitOfWork.Orders.Add(order);

        order.Raise(new OrderCreatedEvent(order));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(order.Id);

    }

    private async Task<Result> ValidateRequest(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        bool employeeExists = await _unitOfWork.Employees.DoesExistByIdAsync(request.employeeId);

        if (!employeeExists)
            return Result.Failed(GovernmentEmployeeErrors.EmployeeNotFound(request.employeeId));


        bool isNumberValid = _phoneNumberChecker.CheckPhoneNumber(request.CustomerPhoneNumber);

        if (!isNumberValid)
            return Result.Failed(OrderErrors.CustomerPhoneNumberIsNotValid(request.CustomerPhoneNumber));

        return Result.Success();
    }

}
