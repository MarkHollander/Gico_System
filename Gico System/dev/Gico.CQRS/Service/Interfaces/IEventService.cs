using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;

namespace Gico.CQRS.Service.Interfaces
{
    public interface IEventService
    {
        Task Add(Event @event);
        Task Add(Event[] @events);

        Task Add(VersionedEvent @event);
        Task Add(VersionedEvent[] @events);
        Task Publish(Event @event);
        Task Publish(Event[] @events);
        Task Publish(VersionedEvent @event);
        Task Publish(VersionedEvent[] @events);

    }
}