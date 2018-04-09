using System;
using System.Collections.Generic;
using System.Text;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class ManufacturerGetResponse: BaseResponse
    {       
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PotalRow { get; set; }
        public ManufacturerViewModel[] Manufacturers { get; set; }
    }
}
