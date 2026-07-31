using HaitikBackend.Application.Common.BulkOrdersModel;

namespace HaitikBackend.Application.Common.Interfaces.Import.Importer;

public interface IDocumentImporter
{
    List<BulkOrderModel> Parse(HaitikBackend.Application.Common.FileModels.FileUpload file);
}
