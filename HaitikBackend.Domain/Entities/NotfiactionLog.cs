using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class NotfiactionLog
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public string Status { get; set; } = null!;

    public short RetryCount { get; set; }

    public bool ProviderResponse { get; set; }
}
