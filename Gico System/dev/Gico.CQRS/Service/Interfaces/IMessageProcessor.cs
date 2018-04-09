using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Gico.CQRS.Service.Interfaces
{
    public interface IMessageProcessor
    {
        void Register(ServiceProvider provider);
        Task<object> Handle(object payload);
        void Start();
    }
}