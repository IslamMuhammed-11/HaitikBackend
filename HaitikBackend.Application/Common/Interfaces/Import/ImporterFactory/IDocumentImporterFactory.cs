using HaitikBackend.Application.Common.Interfaces.Import.Importer;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Application.Common.Interfaces.Import.ImporterFactory;

public interface IDocumentImporterFactory
{
    Result<IDocumentImporter> Get(string ex);
}
