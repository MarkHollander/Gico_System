using System.Threading.Tasks;
using Gico.CQRS.Model.Interfaces;

namespace Gico.CQRS.Service.Interfaces
{
    public interface IMessageHandler
    {
    }
    public interface ICommandHandler<in TI, TO> : IMessageHandler where TI : ICommand where TO : ICommandResult
    {
        Task<TO> Handle(TI message);
    }
    public interface IEventHandler<in TI> : IMessageHandler where TI : IEvent
    {
        Task Handle(TI message);
    }

}