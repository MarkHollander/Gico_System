using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class ManufacturerMapping_GetManufacturerResponse:BaseResponse
    {
        public ManufacturerMapping_GetManufacturerResponse()
        {
            

        }

        public ManufacturerViewModel[] Manufacturers { get; set; }

    }
}

