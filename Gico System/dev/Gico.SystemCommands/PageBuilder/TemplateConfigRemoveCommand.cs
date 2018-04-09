using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.PageBuilder
{
    public class TemplateConfigRemoveCommand : Command
    {
        public string TemplateId { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
    }
}