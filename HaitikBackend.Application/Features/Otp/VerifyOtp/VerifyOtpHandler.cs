using HaitikBackend.Application.Interfaces.Security;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.DomainEvents.OTP;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.Repositories;
using MediatR;

namespace HaitikBackend.Application.Features.Otp.VerifyOtp;

public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, Result>
{
    private readonly IOtpCodeRepository _otpRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IOrderRepository _orderRepository;

    public VerifyOtpHandler(IOtpCodeRepository otpRepository, IPasswordHasher passwordHasher, IOrderRepository orderRepository)
    {
        _otpRepository = otpRepository;
        _passwordHasher = passwordHasher;
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {

        var otpEntity = await _otpRepository.GetByOrderIdAsync(request.OrderId, request.Purpose, cancellationToken);


        var validatingResult = ValidateOTPEntity(otpEntity);

        if (!validatingResult.IsSuccess)
            return validatingResult;

        bool verified = _passwordHasher.VerifyPassword(request.Otp, otpEntity.Otphashed);

        if (!verified)
            return await HandleFailedAttempts(otpEntity, cancellationToken);


        otpEntity.Raise(new OtpVerifiedEvent(otpEntity.OrderId, otpEntity.Id));

        await _otpRepository.SaveChangesAsync(cancellationToken);

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

        await _otpRepository.SaveChangesAsync(ct);

        return result;
    }



}
