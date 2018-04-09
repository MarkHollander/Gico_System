using System.Collections.Generic;
using System.Threading.Tasks;
using Gico.CQRS.Model.Interfaces;

namespace Gico.CQRS.Service.Interfaces
{
    public interface IEventSender
    {
        void Add(IEvent @event);
        void Add(List<IEvent> events);
        Task Notify(List<IEvent> events);
       Task Notify();
    }
}