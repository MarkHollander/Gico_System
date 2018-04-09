using Gico.Config;
using Gico.Models.Request;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Request
{
    public class ManufacturerMapping_GetManufacturerRequest:BaseRequest
    {
        public string CategoryId { get; set; }
    }
}
