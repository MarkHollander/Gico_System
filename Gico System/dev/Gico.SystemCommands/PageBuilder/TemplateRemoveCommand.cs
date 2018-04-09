using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.PageBuilder
{
    public class TemplateRemoveCommand : Command
    {
        public string Id { get; set; }
        public string UserId { get; set; }
    }
}