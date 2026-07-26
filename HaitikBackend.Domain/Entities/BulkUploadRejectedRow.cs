using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class BulkUploadRejectedRow
{
    public int Id { get; set; }

    public int BatchId { get; set; }

    public string Reason { get; set; } = null!;

    public string Row { get; set; } = null!;

    public virtual BulkUploadBatch Batch { get; set; } = null!;
}
