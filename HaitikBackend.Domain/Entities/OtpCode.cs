using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Domain.Entities;

public partial class OtpCode : BaseEntity
{
    public int Id { get; private set; }

    public int OrderId { get; private set; }
    public string Otphashed { get; private set; } = null!;

    public DateTime ExpiryDate { get; private set; }

    public string Purpose { get; private set; } = null!;

    public short AttemptCount { get; private set; }

    private OtpCode()
    {
    }

    private OtpCode(int orderId, string otpHashed, DateTime expiryDate, string purpose, short attemptCount)
    {
        OrderId = orderId;
        Otphashed = otpHashed;
        ExpiryDate = expiryDate;
        Purpose = purpose;
        AttemptCount = attemptCount;
    }


    public static Result<OtpCode> Create(int orderId, string otpHashed, DateTime expiryDate, string purpose, short attemptCount = 0)
    {
        return Result<OtpCode>.Success(
            new OtpCode(orderId, otpHashed, expiryDate, purpose, attemptCount));
    }

    public int RaiseAttemptCount()
    {
        AttemptCount++;

        return AttemptCount;
    }


    public virtual Order Order { get; private set; } = null!;
}
