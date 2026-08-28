using HaitikBackend.Application.Abstractions;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.DomainEvents.OTP;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;
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
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result.Failed(OrderErrors.OrderNotFound(request.OrderId));

        var otp = CreateOtp(order, request.Purpose);

        _unitOfWork.OtpCodes.Add(otp);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }


    private OtpCode CreateOtp(Order order, enOtpPurpose purpose)
    {
        const int OtpValidityMinutes = 5;

        var otpCode = _otpGenerator.GenerateOTP();

        var expiry = DateTime.UtcNow.AddMinutes(OtpValidityMinutes);

        var hashed = _passwordHasher.HashPassword(otpCode);

        var otp = OtpCode.Create(order.Id, hashed, expiry, purpose);



        otp.Raise(new OtpCreatedEvent(order.Id, order.CustomerEmail, otpCode));

        return otp;
    }

}
