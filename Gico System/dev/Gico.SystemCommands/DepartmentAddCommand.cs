using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class DepartmentAddCommand : Command
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public EnumDefine.DepartmentStatusEnum Status { get; set; }
    }
    public class DepartmentChangeCommand : DepartmentAddCommand
    {
    }

   
}