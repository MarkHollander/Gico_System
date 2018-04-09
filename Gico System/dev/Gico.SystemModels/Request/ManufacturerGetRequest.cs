using System;
using System.Collections.Generic;
using System.Text;
using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class ManufacturerGetRequest : BaseRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRow { get; set; }

    }
}
