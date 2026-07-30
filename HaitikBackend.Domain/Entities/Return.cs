using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Entities;

public partial class Return
{

    public int Id { get; private set; }

    public int OrderId { get; private set; }

    public int AgencyId { get; private set; }

    public int? AcceptedById { get; private set; }

    public string Reason { get; private set; } = null!;

    public enReturnStatus Status { get; private set; }

    private Return()
    {
    }

    private Return(int orderId, int initiatedById,
        int? acceptedById, string reason, enReturnStatus status)
    {
        OrderId = orderId;
        AgencyId = initiatedById;
        AcceptedById = acceptedById;
        Reason = reason;
        Status = status;
    }


    public static Return ReturnRequest(int orderId, int initiatedById, string reason)
    {
        return new Return(orderId, initiatedById, null,
                          reason, enReturnStatus.Pending);
    }


    public void AcceptReturn(int acceptedBy)
    {
        if (Status != enReturnStatus.Pending)
            return;

        ChangeStatus(enReturnStatus.Accepted);

        AcceptedById = acceptedBy;
    }


    public void RejectReturn()
    {
        if (Status != enReturnStatus.Pending)
            return;

        ChangeStatus(enReturnStatus.Rejected);

        AcceptedById = null;
    }

    private void ChangeStatus(enReturnStatus status)
    {
        Status = status;
    }

    public virtual Order Order { get; private set; } = null!;

    public virtual GovernmentAgency Agency { get; private set; } = null!;

    public virtual DeliveryAdmin DeliveryAdmin { get; private set; } = null!;


}
