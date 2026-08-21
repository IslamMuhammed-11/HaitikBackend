using HaitikBackend.Application.Common.Models.FileModels;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Application.Abstractions;

public interface IFileStorage
{

    Task<Result<FileResult>> UploadAsync(
        FileUpload file, CancellationToken ct = default);
}
