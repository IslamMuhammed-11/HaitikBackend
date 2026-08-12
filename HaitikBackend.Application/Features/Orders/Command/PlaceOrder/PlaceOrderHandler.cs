using HaitikBackend.Application.Common.Interfaces.PhoneNumberChecker;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.DomainEvents.OrderEvents;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using HaitikBackend.Domain.ValueObjects;
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

        var pickupLocation = GeoLocation.Create(request.Latitude, request.Longitude);


        var order = Order.Create(request.CustomerPhoneNumber, DateTime.UtcNow, pickupLocation, request.AgencyId);

        _unitOfWork.Orders.Add(order);

        order.Raise(new OrderCreatedEvent(order));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(order.Id);

    }

    private async Task<Result> ValidateRequest(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        bool agencyExists = await _unitOfWork.Agencies.DoesExistAsync(request.AgencyId);

        if (!agencyExists)
            return Result.Failed(GovernmentAgencyErrors.AgencyNotFound(request.AgencyId));

        bool isNumberValid = _phoneNumberChecker.CheckPhoneNumber(request.CustomerPhoneNumber);

        if (!isNumberValid)
            return Result.Failed(OrderErrors.CustomerPhoneNumberIsNotValid(request.CustomerPhoneNumber));

        return Result.Success();
    }

}
