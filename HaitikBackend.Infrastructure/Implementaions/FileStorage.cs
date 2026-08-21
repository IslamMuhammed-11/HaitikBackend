using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Common.Models.FileModels;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Infrastructure.Implementaions;

public class FileStorage : IFileStorage
{
    public Task<Result<FileResult>> UploadAsync(
        Application.Common.Models.FileModels.FileUpload file, CancellationToken ct = default)
        => throw new NotImplementedException();
}
