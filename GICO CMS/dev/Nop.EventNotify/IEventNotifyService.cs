using System.Threading.Tasks;

namespace Nop.EventNotify
{
    public interface IEventNotifyService
    {
        Task Notify<T>(T @event);
    }
}