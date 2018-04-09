namespace Gico.CQRS.Model.Interfaces
{
    public interface IVersionedEvent : IEvent
    {
        int Version { get; }
    }
}