using HaitikBackend.Application.Abstractions.Import.Importer;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Application.Abstractions.Import.ImporterFactory;

public interface IDocumentImporterFactory
{
    Result<IDocumentImporter> Get(string ex);
}
