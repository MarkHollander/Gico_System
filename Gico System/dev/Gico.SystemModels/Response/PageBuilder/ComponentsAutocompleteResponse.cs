using Gico.Config;
using Gico.Models.Response;

namespace Gico.SystemModels.Response.PageBuilder
{
    public class ComponentsAutocompleteResponse : BaseResponse
    {
        public KeyValueTypeStringModel[] Components { get; set; }
    }
}