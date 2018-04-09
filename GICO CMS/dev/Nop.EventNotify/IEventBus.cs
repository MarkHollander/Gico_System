using System.Threading.Tasks;

namespace Nop.EventNotify
{
    public interface IEventBus
    {
        Task Notify(Message message);
    }
}