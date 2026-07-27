using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Application.Features.Otp.VerifyOtp;

public sealed record VerifyOtpCommand(int OrderId, string Otp , enOtpPurpose Purpose) : IRequest<Result>;
