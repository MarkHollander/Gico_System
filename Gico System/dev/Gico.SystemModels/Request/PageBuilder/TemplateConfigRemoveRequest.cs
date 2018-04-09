using Gico.Models.Request;

namespace Gico.SystemModels.Request.PageBuilder
{
    public class TemplateConfigRemoveRequest : BaseRequest
    {
        public string TemplateId { get; set; }
        public string Id { get; set; }
    }
}