namespace Gico.CQRS.Model.Interfaces
{
    public interface IEvent
    {
        string EventId { get; }
    }
}