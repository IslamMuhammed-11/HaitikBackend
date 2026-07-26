using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Domain.Entities;

public partial class OtpCode : BaseEntity
{
    public int Id { get; private set; }

    public string Otphashed { get; private set; } = null!;

    public DateTime ExpiryDate { get; private set; }

    public string Purpose { get; private set; } = null!;

    public short AttemptCount { get; private set; }

    private OtpCode()
    {
    }

    private OtpCode(string otpHashed, DateTime expiryDate, string purpose, short attemptCount)
    {
        Otphashed = otpHashed;
        ExpiryDate = expiryDate;
        Purpose = purpose;
        AttemptCount = attemptCount;
    }


    public static Result<OtpCode> Create(string otpHashed, DateTime expiryDate, string purpose, short attemptCount = 0)
    {
        return Result<OtpCode>.Success(
            new OtpCode(otpHashed, expiryDate, purpose, attemptCount));
    }

    public int RaiseAttemptCount()
    {
        AttemptCount++;

        return AttemptCount;
    }
}
