using HaitikBackend.Application.Common.Interfaces.OTP;
using HaitikBackend.Application.Common.Interfaces.Security;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.DomainEvents.OTP;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.Otp.CreateOtp;

public class CreateOtpHandler : IRequestHandler<CreateOtpCommand, Result>
{
    private readonly IOtpGenerator _otpGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOtpHandler(IOtpGenerator otpGenerator, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _otpGenerator = otpGenerator;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateOtpCommand request, CancellationToken cancellationToken)
    {
        bool orderExist = await _unitOfWork.Orders.DoesExist(request.OrderId, cancellationToken);

        if (!orderExist)
            return Result.Failed(OrderErrors.OrderNotFound(request.OrderId));

        var otp = CreateOtp(request.OrderId, request.Purpose);

        _unitOfWork.OtpCodes.Add(otp);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }


    private OtpCode CreateOtp(int orderId, enOtpPurpose purpose)
    {
        const int OtpValidityMinutes = 5;

        var otpCode = _otpGenerator.GenerateOTP();

        var expiry = DateTime.UtcNow.AddMinutes(OtpValidityMinutes);

        var hashed = _passwordHasher.HashPassword(otpCode);

        var otp = OtpCode.Create(orderId, hashed, expiry, purpose);

        

        otp.Raise(new OtpCreatedEvent(orderId, otpCode));

        return otp;
    }

}
