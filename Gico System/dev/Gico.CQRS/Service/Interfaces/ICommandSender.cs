using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;

namespace Gico.CQRS.Service.Interfaces
{
    public interface ICommandSender
    {
        Task<string> Send(Command command, bool isSendResult = false);
        Task<T> SendAndReceiveResult<T>(Command command);
    }
}