using HaitikBackend.Application.Abstractions;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.DomainEvents.OrderEvents;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.ValueObjects;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.PlaceOrder;

public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, Result<PlaceOrderResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhoneNumberChecker _phoneNumberChecker;
    private readonly ITokenService _tokenService;

    private readonly IPasswordHasher _passwordHasher;
    public PlaceOrderHandler(IUnitOfWork unitOfWork, IPhoneNumberChecker phoneNumberChecker, ITokenService tokenService, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _phoneNumberChecker = phoneNumberChecker;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<PlaceOrderResponse>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {

        var validatorResult = await ValidateRequest(request, cancellationToken);

        if (!validatorResult.IsSuccess)
            return Result<PlaceOrderResponse>.Failed(validatorResult.Error!);

        var pickupLocation = GeoLocation.Create(request.Latitude, request.Longitude);


        var token = _tokenService.GenerateRefreshToken();

        var hashedtoken = _passwordHasher.HashPassword(token);

        var order = Order.Create(request.CustomerPhoneNumber, DateTime.UtcNow, pickupLocation, request.AgencyId, hashedtoken);

        _unitOfWork.Orders.Add(order);

        order.Raise(new OrderCreatedEvent(order));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new PlaceOrderResponse(order.Id, token);

        return Result<PlaceOrderResponse>.Success(response);

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
