namespace HaitikBackend.Domain.Entities;

public partial class BulkUploadBatch : BaseEntity
{
    public int Id { get; private set; }

    public int UploadedBy { get; private set; }

    public int Counts { get; private set; }

    public string Status { get; private set; } = null!;

    private BulkUploadBatch()
    {
    }

    private BulkUploadBatch(int uploadedBy, int counts, string status)
    {
        UploadedBy = uploadedBy;
        Counts = counts;
        Status = status;
    }


    public static BulkUploadBatch Create(int uploadedBy, int counts, string status)
    {
        return new BulkUploadBatch(uploadedBy, counts, status);
    }

    public virtual ICollection<BulkUploadRejectedRow> BulkUploadRejectedRows { get; private set; } = new List<BulkUploadRejectedRow>();
}
