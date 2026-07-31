namespace HaitikBackend.Domain.Entities;

public partial class DeliveryProof : BaseEntity
{
    public int OrderId { get; private set; }

    public string ImageUrl { get; private set; } = null!;

    public string ReciverName { get; private set; } = null!;

    public string? DeliveryNotes { get; private set; } = null!;

    public DateTime DeliverdAt { get; private set; }

    private DeliveryProof()
    {
    }

    private DeliveryProof(int orderId, string imageUrl, string reciverName, string? deliveryNotes, DateTime deliveredAt)
    {
        OrderId = orderId;
        ImageUrl = imageUrl;
        ReciverName = reciverName;
        DeliveryNotes = deliveryNotes;
        DeliverdAt = deliveredAt;
    }

    internal static DeliveryProof Create(int orderId, string imageUrl, string reciverName, string? deliveryNotes, DateTime deliveredAt)
    {
        return new DeliveryProof(orderId, imageUrl, reciverName, deliveryNotes, deliveredAt);


    }

    public void UpdateDeliveryNotes(string? deliveryNotes)
    {
        DeliveryNotes = deliveryNotes;
    }

    public virtual Order Order { get; private set; } = null!;
}
