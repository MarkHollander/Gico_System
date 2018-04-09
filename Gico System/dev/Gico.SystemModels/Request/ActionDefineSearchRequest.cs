using Gico.Config;

namespace Gico.SystemModels.Request
{
    public class ActionDefineSearchRequest
    {
        public string Group { get; set; }
        public int PageIndex { get; set; }
        public string RoleId { get; set; }
    }
}