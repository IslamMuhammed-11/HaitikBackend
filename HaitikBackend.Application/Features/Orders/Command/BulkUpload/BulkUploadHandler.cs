using HaitikBackend.Application.Common.Interfaces.Import.ImporterFactory;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.BulkUpload;

public class BulkUploadHandler : IRequestHandler<BulkUploadCommand, Result>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentImporterFactory _importerFactory;

    public BulkUploadHandler(IUnitOfWork unitOfWork, IDocumentImporterFactory documentImporterFactory)
    {
        _importerFactory = documentImporterFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(BulkUploadCommand request, CancellationToken cancellationToken)
    {

        var importerResult = _importerFactory.Get(request.File.Extension);

        if (!importerResult.IsSuccess)
            return Result.Failed(importerResult.Error!);

        var orders = importerResult.Value!.Parse(request.File);

        foreach (var order in orders)
        {
            var entity = Order.Create(order.CustomerPhoneNumber, DateTime.Now, order.DeliveryLocation, order.AgencyId);

            _unitOfWork.Orders.Add(entity);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
