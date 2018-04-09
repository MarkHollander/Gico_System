using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request
{
    public class CategoryAttrRequest
    {
        public string Id { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
