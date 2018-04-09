using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class RoleActionMappingChangeByRoleCommand : Command
    {
        public string RoleId { get; set; }
        public string[] ActionIdsAdd { get; set; }
        public string[] ActionIdsRemove { get; set; }
    }
}