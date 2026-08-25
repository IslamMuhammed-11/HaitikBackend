using HaitikBackend.Application.Common.Models.BulkOrdersModel;

namespace HaitikBackend.Application.Abstractions;

public interface IDocumentImporter
{
    BulkUploadResult Parse(Stream file);
}
