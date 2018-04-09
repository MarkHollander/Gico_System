using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request.PageBuilder
{
    public class TemplateConfigGetRequest : BaseRequest
    {
        public string Id { get; set; }
        public string TemplateId { get; set; }
    }
}
