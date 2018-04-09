using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class ActionDefineAddCommand : Command
    {
        public string Id { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
    }
}