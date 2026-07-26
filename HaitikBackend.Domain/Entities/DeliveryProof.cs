namespace HaitikBackend.Domain.Entities;

using HaitikBackend.Domain.Common.Results;

public partial class DeliveryProof : BaseEntity
{
    public int Id { get; private set; }

    public int OrderId { get; private set; }

    public string ImageUrl { get; private set; } = null!;

    public string ReciverName { get; private set; } = null!;

    public string DeliveryNotes { get; private set; } = null!;

    public DateTime DeliverdAt { get; private set; }

    private DeliveryProof()
    {
    }

    private DeliveryProof(int orderId, string imageUrl, string reciverName, string deliveryNotes, DateTime deliveredAt)
    {
        OrderId = orderId;
        ImageUrl = imageUrl;
        ReciverName = reciverName;
        DeliveryNotes = deliveryNotes;
        DeliverdAt = deliveredAt;
    }

    internal static Result<DeliveryProof> Create(int orderId, string imageUrl, string reciverName, string deliveryNotes, DateTime deliveredAt)
    {
        var proof = new DeliveryProof(orderId, imageUrl, reciverName, deliveryNotes, deliveredAt);

        return Result<DeliveryProof>.Success(proof);
    }

    public virtual Order Order { get; private set; } = null!;
}
