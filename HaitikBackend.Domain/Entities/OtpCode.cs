using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class OtpCode
{
    public int Id { get; set; }

    public string Otphashed { get; set; } = null!;

    public DateTime ExpiryDate { get; set; }

    public string Purpose { get; set; } = null!;

    public short AttemptCount { get; set; }
}
