using HaitikBackend.Application.Abstractions.Import.Importer;
using HaitikBackend.Application.Abstractions.Import.ImporterFactory;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Infrastructure.Implementaions;

public class DocumentImporterFactory : IDocumentImporterFactory
{
    public Result<IDocumentImporter> Get(string ex)
        => throw new NotImplementedException();
}
