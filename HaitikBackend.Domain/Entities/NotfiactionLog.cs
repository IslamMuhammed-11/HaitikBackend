using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Domain.Entities;

public partial class NotfiactionLog : BaseEntity
{
    public int Id { get; private set; }

    public string Content { get; private set; } = null!;

    public string Status { get; private set; } = null!;

    public short RetryCount { get; private set; }

    public bool ProviderResponse { get; private set; }

    private NotfiactionLog()
    {
    }

    private NotfiactionLog(string content, string status, short retryCound, bool providerResponse)
    {
        Content = content;
        Status = status;
        RetryCount = retryCound;
        ProviderResponse = providerResponse;
    }

    public static Result<NotfiactionLog> Create(string content, string status, short retryCound, bool providerResponse)
    {
        return Result<NotfiactionLog>.Success(
            new NotfiactionLog(content, status, retryCound, providerResponse));

    }
}
