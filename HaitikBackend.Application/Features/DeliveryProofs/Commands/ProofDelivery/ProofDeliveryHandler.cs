using HaitikBackend.Application.Abstractions;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;

namespace HaitikBackend.Application.Features.DeliveryProofs.Commands.ProofDelivery;

public class ProofDeliveryHandler : IRequestHandler<ProofDeliveryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;

    public ProofDeliveryHandler(IUnitOfWork unitOfWork, IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ProofDeliveryCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.orderId, cancellationToken);

        if (order is null)
            return Result.Failed(OrderErrors.OrderNotFound(request.orderId));

        var storeImageResult = await _fileStorage.UploadAsync(request.file, cancellationToken);

        if (!storeImageResult.IsSuccess)
            return Result.Failed(storeImageResult.Error!);

        var result = order.ProofDelivery(storeImageResult.Value!.Url, request.reciverName, request.deliveryNotes, DateTime.Now);

        if (!result.IsSuccess)
            return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }




}

