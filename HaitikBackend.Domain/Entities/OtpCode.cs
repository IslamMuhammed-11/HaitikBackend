using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;

namespace HaitikBackend.Domain.Entities;

public partial class OtpCode : BaseEntity
{
    public int Id { get; private set; }

    public int OrderId { get; private set; }
    public string Otphashed { get; private set; } = null!;

    public DateTime ExpiryDate { get; private set; }

    public enOtpPurpose Purpose { get; private set; }

    public short AttemptCount { get; private set; }

    private OtpCode()
    {
    }

    private OtpCode(int orderId, string otpHashed, DateTime expiryDate, enOtpPurpose purpose, short attemptCount)
    {
        OrderId = orderId;
        Otphashed = otpHashed;
        ExpiryDate = expiryDate;
        Purpose = purpose;
        AttemptCount = attemptCount;
    }


    public static OtpCode Create(int orderId, string otpHashed, DateTime expiryDate, enOtpPurpose purpose, short attemptCount = 0)
    {
        return new OtpCode(orderId, otpHashed, expiryDate, purpose, attemptCount);
    }

    public Result RecordFailedAttempt()
    {
        AttemptCount++;

        if (AttemptCount >= MaximumAttempts)
            return Result.Failed(OtpErrors.MaxAttemptsReached);


        return Result.Failed(OtpErrors.OtpInvalid);
    }

    public bool IsExpired(DateTime date) => ExpiryDate > date;

    public const int MaximumAttempts = 10;

    public const int OtpLength = 6;

    public virtual Order Order { get; private set; } = null!;
}
