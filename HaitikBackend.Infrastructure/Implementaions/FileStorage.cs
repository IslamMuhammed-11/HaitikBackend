using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Common.Models.FileModels;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Infrastructure.Implementaions;

public class FileStorage : IFileStorage
{

    const string directory = @"C:\Users\Eslam\Desktop\pod";

    public async Task<Result<FileResult>> UploadAsync(
        FileUpload file, CancellationToken ct = default)
    {

        try
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var filename = $"{Guid.NewGuid()}{file.Extension}";

            var path = Path.Combine(directory, filename);

            await using var stream = File.Create(path);
            await file.Content.CopyToAsync(stream);

            var result = new FileResult(path, filename);

            return Result<FileResult>.Success(result);

        }
        catch (Exception)
        {

            throw;
        }

    }
}
