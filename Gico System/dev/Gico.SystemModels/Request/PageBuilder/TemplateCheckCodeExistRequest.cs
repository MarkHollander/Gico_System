using Gico.Models.Request;

namespace Gico.SystemModels.Request.PageBuilder
{
    public class TemplateCheckCodeExistRequest : BaseRequest
    {
        public string TemplateId { get; set; }
        public string Code { get; set; }
        public string CurrentTemplateConfigId { get; set; }
    }
}