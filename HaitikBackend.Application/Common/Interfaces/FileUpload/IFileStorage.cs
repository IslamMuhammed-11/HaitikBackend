using HaitikBackend.Application.Common.FileModels;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Application.Common.Interfaces.FileUpload;

public interface IFileStorage
{

    Task<Result<FileResult>> UploadAsync(
        FileModels.FileUpload file, CancellationToken ct = default);
}
