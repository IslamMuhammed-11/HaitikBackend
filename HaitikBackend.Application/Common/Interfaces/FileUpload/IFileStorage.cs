using HaitikBackend.Application.Common.Models.FileModels;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Application.Common.Interfaces.FileUpload;

public interface IFileStorage
{

    Task<Result<FileResult>> UploadAsync(
        Models.FileModels.FileUpload file, CancellationToken ct = default);
}
