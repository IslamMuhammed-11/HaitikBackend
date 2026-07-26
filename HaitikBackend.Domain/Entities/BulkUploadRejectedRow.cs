namespace HaitikBackend.Domain.Entities;

using HaitikBackend.Domain.Common.Results;

public partial class BulkUploadRejectedRow : BaseEntity
{
    public int Id { get; private set; }

    public int BatchId { get; private set; }

    public string Reason { get; private set; } = null!;

    public string Row { get; private set; } = null!;

    private BulkUploadRejectedRow()
    {
    }

    private BulkUploadRejectedRow(int batchId, string reason, string row)
    {
        BatchId = batchId;
        Reason = reason;
        Row = row;
    }

    public static Result<BulkUploadRejectedRow> Create(int batchId, string reason, string row)
    {
        var entity = new BulkUploadRejectedRow(batchId, reason, row);

        return Result<BulkUploadRejectedRow>.Success(entity);
    }

    public virtual BulkUploadBatch Batch { get; private set; } = null!;
}
