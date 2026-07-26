namespace HaitikBackend.Domain.Entities;

using HaitikBackend.Domain.Common.Results;

public partial class DeliveryAdmin : BaseEntity
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    private DeliveryAdmin()
    {
    }

    private DeliveryAdmin(int userId)
    {
        UserId = userId;
    }

    internal static Result<DeliveryAdmin> Create(int userId)
    {
        var admin = new DeliveryAdmin(userId);

        return Result<DeliveryAdmin>.Success(admin);
    }

    public virtual User User { get; private set; } = null!;
}
