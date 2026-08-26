using HaitikBackend.Application.Abstractions;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.PublicTracking.TrackOrder;

public sealed class TrackOrderHandler : IRequestHandler<TrackOrderQuery, Result<PublicTrackingResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITrackingTokenHasher _tokenHasher;

    public TrackOrderHandler(IUnitOfWork unitOfWork, ITrackingTokenHasher tokenHasher)
    {
        _unitOfWork = unitOfWork;
        _tokenHasher = tokenHasher;
    }

    public async Task<Result<PublicTrackingResponse>> Handle(TrackOrderQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Token))
            return Result<PublicTrackingResponse>.Failed(OrderErrors.InvalidTrackingToken);

        var tokenHash = _tokenHasher.Hash(request.Token);
        var order = await _unitOfWork.Orders.Query()
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.TrackingTokenHash == tokenHash, cancellationToken);

        if (order is null || order.TrackingTokenHash is null || !_tokenHasher.Verify(request.Token, order.TrackingTokenHash))
            return Result<PublicTrackingResponse>.Failed(OrderErrors.InvalidTrackingToken);

        var response = new PublicTrackingResponse(order.Id, order.Status, order.CreatedAt);
        return Result<PublicTrackingResponse>.Success(response);
    }
}
