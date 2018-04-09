using Gico.Config;
using ProtoBuf;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RActionDefine : BaseReadModel
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Group { get;  set; }
    }
    public class RRoleActionMapping : BaseReadModel
    {
        public string RoleId { get; set; }
        public string ActionId { get; set; }
        public string Attributes { get; set; }
    }
    [ProtoContract]
    public class RRole : BaseReadModel
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string DepartmentId { get; set; }
        [ProtoMember(3)]
        public new EnumDefine.RoleStatusEnum Status { get; set; }
    }
    [ProtoContract]
    public class RDepartment : BaseReadModel
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public new EnumDefine.DepartmentStatusEnum Status { get; set; }
    }

    public class RCustomerRoleMapping : BaseReadModel
    {
        public string CustomerId { get; set; }
        public string RoleId { get; set; }
    }
}