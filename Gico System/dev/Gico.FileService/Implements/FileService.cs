using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Gico.FileCommands;
using Gico.FileService.Interfaces;

namespace Gico.FileService.Implements
{
    public class FileService : IFileService
    {
        private readonly ICommandSender _commandService;

        public FileService(ICommandSender commandService)
        {
            _commandService = commandService;
        }

        public async Task ImageAdd(ImageAddCommand command)
        {
            await _commandService.Send(command);
        }
    }
}
