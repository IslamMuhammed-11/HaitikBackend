using HaitikBackend.Domain.DomainEvents.ReturnEvents;
using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Entities;

public partial class Return : BaseEntity
{

    public int OrderId { get; private set; }

    public int AgencyId { get; private set; }

    public int? ReviewedById { get; private set; }

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
        ReviewedById = acceptedById;
        Reason = reason;
        Status = status;
    }


    internal static Return ReturnRequest(int orderId, int agencyId, string reason)
    {

        

        return new Return(orderId, agencyId, null,
                          reason, enReturnStatus.Pending);
    }


    public void AcceptReturn(int acceptedBy)
    {
        if (Status != enReturnStatus.Pending)
            return;

        ChangeStatus(enReturnStatus.Accepted, acceptedBy);


        Raise(new ReturnRequestAcceptedEvent(OrderId, acceptedBy));
    }


    public void RejectReturn(int rejectedBy)
    {
        if (Status != enReturnStatus.Pending)
            return;

        ChangeStatus(enReturnStatus.Rejected, rejectedBy);

    }

    private void ChangeStatus(enReturnStatus status, int userId)
    {
        Status = status;

        ReviewedById = userId;
    }

    public virtual Order Order { get; private set; } = null!;

    public virtual GovernmentAgency Agency { get; private set; } = null!;

    public virtual User? User { get; private set; }


}
