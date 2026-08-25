using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Common.Models.BulkOrdersModel;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.ValueObjects;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.BulkUpload;

public class BulkUploadHandler : IRequestHandler<BulkUploadCommand, Result>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentImporter _importer;

    public BulkUploadHandler(IUnitOfWork unitOfWork, IDocumentImporter importer)
    {
        _unitOfWork = unitOfWork;
        _importer = importer;
    }

    public async Task<Result> Handle(BulkUploadCommand request, CancellationToken cancellationToken)
    {
        if (!request.File.Validate())
            return Result.Failed(BulkUploadErrors.UploadFailed);


        BulkUploadResult result = _importer.Parse(request.File.Content);

        var batch = BulkUploadBatch.Create(request.UploadedBy, result.orders.Count, "Uploaded");

        _unitOfWork.BulkUploadBatchs.Add(batch);

        var orders = new List<Order>();

        foreach (var order in result.orders)
        {

            var location = GeoLocation.Create(order.Latitude, order.Longitude);

            var orderCreation = Order.Create(order.CustomerPhoneNumber, DateTime.UtcNow, location, request.UploadedBy, null);

            orders.Add(orderCreation);
        }

        _unitOfWork.Orders.AddRange(orders);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
