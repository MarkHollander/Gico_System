namespace Gico.SystemModels.Request
{
    public class PermissionChangeByRoleRequest
    {
        public string RoleId { get; set; }
        public string[] ActionIdsAdd { get; set; }
        public string[] ActionIdsRemove { get; set; }
    }
}