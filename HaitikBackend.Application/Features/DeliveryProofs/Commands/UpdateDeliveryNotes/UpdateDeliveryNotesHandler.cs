using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;

namespace HaitikBackend.Application.Features.DeliveryProofs.Commands.UpdateDeliveryNotes;

public class UpdateDeliveryNotesHandler : IRequestHandler<UpdateDeliveryNotesCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDeliveryNotesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateDeliveryNotesCommand request, CancellationToken cancellationToken)
    {
        var proof = await _unitOfWork.DeliveryProofs.GetByIdAsync(request.OrderId, cancellationToken);

        if (proof is null)
            return Result.Failed(DeliveryProofErrors.DeliveryProofNotFound(request.OrderId));

        proof.UpdateDeliveryNotes(request.DeliveryNotes);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
