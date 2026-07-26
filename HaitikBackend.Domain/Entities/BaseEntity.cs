using MediatR;

namespace HaitikBackend.Domain.Entities;

public class BaseEntity
{
    private List<INotification> _domainEvents = new List<INotification>();

    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public void Raise(INotification notification) => _domainEvents.Add(notification);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
