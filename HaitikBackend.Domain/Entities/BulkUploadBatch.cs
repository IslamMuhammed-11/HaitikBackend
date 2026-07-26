using HaitikBackend.Infrastructure;
using System;
using System.Collections.Generic;

namespace HaitikBackend.Domain.Entities;

public partial class BulkUploadBatch
{
    public int Id { get; set; }

    public int UploadedBy { get; set; }

    public short Counts { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<BulkUploadRejectedRow> BulkUploadRejectedRows { get; set; } = new List<BulkUploadRejectedRow>();
}
