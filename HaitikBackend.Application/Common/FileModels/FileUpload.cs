namespace HaitikBackend.Application.Common.FileModels;

public sealed class FileUpload
{
    public Stream Content { get; init; } = null!;

    public string FileName { get; init; } = null!;

    public string ContentType { get; init; } = null!;

    public string Extension { get; init; } = null!;

    public long Length { get; init; }



    public bool Validate()
    {
        return Content.CanRead && !string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(ContentType);
    }
}
