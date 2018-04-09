using Gico.CQRS.Model.Implements;

namespace Gico.EmailOrSmsCommands
{
    public class EmailSmsSendCommand : Command
    {
        public string Id { get; set; }
    }
}