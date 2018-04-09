using Gico.Config;
using Gico.Models.Request;

namespace Gico.SystemModels.Request.PageBuilder
{
    public class ComponentsAutocompleteRequest : BaseRequest
    {
        public EnumDefine.TemplateConfigComponentTypeEnum ComponentType { get; set; }
        public string Tearm { get; set; }
        
    }
}