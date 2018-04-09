using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class RoleAddCommand : Command
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public EnumDefine.RoleStatusEnum Status { get; set; }
        public string DepartmentId { get; set; }
    }
    public class RoleChangeCommand : RoleAddCommand
    {
    }
}