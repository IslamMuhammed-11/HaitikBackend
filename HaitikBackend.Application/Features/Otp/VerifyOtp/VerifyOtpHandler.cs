using HaitikBackend.Application.Common.Interfaces.Security;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.DomainEvents.OTP;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.Otp.VerifyOtp;

public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, Result>
{

    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;



    public VerifyOtpHandler(IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {

        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;

    }

    public async Task<Result> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {

        var otpEntity = await _unitOfWork.OtpCodes.GetByOrderIdAsync(request.OrderId, request.Purpose, cancellationToken);


        var validatingResult = ValidateOTPEntity(otpEntity);

        if (!validatingResult.IsSuccess)
            return validatingResult;

        bool verified = _passwordHasher.VerifyPassword(request.Otp, otpEntity.Otphashed);

        if (!verified)
            return await HandleFailedAttempts(otpEntity, cancellationToken);


        var orderResult = await SetOrderAsDelivered(request.OrderId);

        if(orderResult.IsSuccess is false)
            return orderResult;

        otpEntity.Raise(new OtpVerifiedEvent(otpEntity.OrderId, otpEntity.Id));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<Result> SetOrderAsDelivered(int orderId)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId, default);

        if (order is null)
            return Result.Failed(OrderErrors.OrderNotFound(orderId));

        order.SetStatusAsDelivered();

        return Result.Success();

    }

    private Result ValidateOTPEntity(OtpCode otp)
    {
        if (otp is null)
            return Result.Failed(OtpErrors.OtpNotFound);

        if (otp.IsExpired(DateTime.UtcNow))
            return Result.Failed(OtpErrors.OtpExpired);

        return Result.Success();
    }

    private async Task<Result> HandleFailedAttempts(OtpCode otpEntity, CancellationToken ct)
    {

        var result = otpEntity.RecordFailedAttempt();

        await _unitOfWork.SaveChangesAsync(ct);

        return result;
    }



}
