using Gico.Models.Response;

namespace Gico.SystemModels.Response
{
    public class RoleActionMappingsResponse : BaseResponse
    {
        public string[] ActionIds { get; set; }
        public bool IsAdministrator { get; set; }
    }
}