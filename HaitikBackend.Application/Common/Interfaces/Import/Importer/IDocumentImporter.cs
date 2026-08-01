using HaitikBackend.Application.Common.Models.BulkOrdersModel;

namespace HaitikBackend.Application.Common.Interfaces.Import.Importer;

public interface IDocumentImporter
{
    List<BulkOrderModel> Parse(Models.FileModels.FileUpload file);
}
