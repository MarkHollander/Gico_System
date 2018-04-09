using System.Threading.Tasks;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;

namespace Gico.CQRS.Service.Implements
{
    public class CommandSender : ICommandSender
    {
        private readonly ICommandBus _bus;

        public CommandSender(ICommandBus bus)
        {
            _bus = bus;
        }

        public async Task<string> Send(Command command, bool isSendResult = false)
        {
            Message message = new Message(command) {IsSendResult = isSendResult};
            return await _bus.Send(message, true);
        }
        public async Task<T> SendAndReceiveResult<T>(Command command)
        {
            var resultKey = await Send(command,true);
            var result = await _bus.ReceiveCommandResult(resultKey);
            return (T)result;
        }


    }
}