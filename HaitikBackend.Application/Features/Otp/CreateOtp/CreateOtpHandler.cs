using HaitikBackend.Application.Interfaces.OTP;
using HaitikBackend.Application.Interfaces.Security;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.DomainEvents.OTP;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.Repositories;
using MediatR;

namespace HaitikBackend.Application.Features.Otp.CreateOtp;

public class CreateOtpHandler : IRequestHandler<CreateOtpCommand, Result>
{
    private readonly IOtpGenerator _otpGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IOtpCodeRepository _otpRepository;
    private readonly IOrderRepository _orderRepository;

    public CreateOtpHandler(IOtpGenerator otpGenerator, IPasswordHasher passwordHasher, IOtpCodeRepository otpRepository, IOrderRepository orderRepository)
    {
        _otpGenerator = otpGenerator;
        _passwordHasher = passwordHasher;
        _otpRepository = otpRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(CreateOtpCommand request, CancellationToken cancellationToken)
    {
        bool orderExist = await _orderRepository.DoesExist(request.OrderId, cancellationToken);

        if (!orderExist)
            return Result.Failed(OrderErrors.OrderNotFound(request.OrderId));

        var otp = CreateOtp(request.OrderId, request.Purpose);

        _otpRepository.Add(otp);

        await _otpRepository.SaveChangesAsync(cancellationToken);

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
