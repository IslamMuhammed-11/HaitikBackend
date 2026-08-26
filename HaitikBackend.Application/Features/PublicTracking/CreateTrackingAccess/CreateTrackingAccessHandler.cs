using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Common.Models.Email;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.PublicTracking.CreateTrackingAccess;

public sealed class CreateTrackingAccessHandler : IRequestHandler<CreateTrackingAccessCommand, Result<string>>
{
    private const string TrackingUrlTemplate = "https://localhost:7027/api/Public/Track/{0}";

    private readonly IUnitOfWork _unitOfWork;
    private readonly ITrackingTokenGenerator _tokenGenerator;
    private readonly ITrackingTokenHasher _tokenHasher;

    private readonly IEmailService _email;

    public CreateTrackingAccessHandler(
        IUnitOfWork unitOfWork,
        ITrackingTokenGenerator tokenGenerator,
        ITrackingTokenHasher tokenHasher,
        IEmailService email)
    {
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;
        _tokenHasher = tokenHasher;
        _email = email;
    }

    public async Task<Result<string>> Handle(CreateTrackingAccessCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null || order.TrackingTokenHash is not null)
            return Result<string>.Success(string.Empty);

        var rawToken = _tokenGenerator.Generate();
        var hashedToken = _tokenHasher.Hash(rawToken);

        order.SetTrackingToken(hashedToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (request.email is not null)
        {
            var message = EmailMessage.Create("Order is On the Way!", $"Your order will be there Soon!,\n You can track Your order through this Link!\n {rawToken}", request.email);

            await _email.SendEmailAsync(message);
        }

        return Result<string>.Success(string.Format(TrackingUrlTemplate, rawToken));
    }
}
