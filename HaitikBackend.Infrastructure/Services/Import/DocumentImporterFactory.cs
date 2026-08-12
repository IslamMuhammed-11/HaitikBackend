using HaitikBackend.Application.Common.Interfaces.Import.Importer;
using HaitikBackend.Application.Common.Interfaces.Import.ImporterFactory;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Infrastructure.Services.Import;

public class DocumentImporterFactory : IDocumentImporterFactory
{
    public Result<IDocumentImporter> Get(string ex)
        => throw new NotImplementedException();
}
