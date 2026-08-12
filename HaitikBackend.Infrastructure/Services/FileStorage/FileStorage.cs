using HaitikBackend.Application.Common.Interfaces.FileUpload;
using HaitikBackend.Application.Common.Models.FileModels;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Infrastructure.Services.FileStorage;

public class FileStorage : IFileStorage
{
    public Task<Result<FileResult>> UploadAsync(
        Application.Common.Models.FileModels.FileUpload file, CancellationToken ct = default)
        => throw new NotImplementedException();
}
