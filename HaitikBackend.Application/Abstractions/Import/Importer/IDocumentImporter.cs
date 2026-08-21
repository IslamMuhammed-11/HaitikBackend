using HaitikBackend.Application.Common.Models.BulkOrdersModel;

namespace HaitikBackend.Application.Abstractions.Import.Importer;

public interface IDocumentImporter
{
    List<BulkOrderModel> Parse(Common.Models.FileModels.FileUpload file);
}
