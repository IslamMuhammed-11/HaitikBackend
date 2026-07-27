using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Application.Features.Otp.CreateOtp;

public sealed record CreateOtpCommand(int OrderId, enOtpPurpose Purpose) : IRequest<Result>;
