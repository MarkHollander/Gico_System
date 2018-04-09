using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.EmailOrSmsModel.Request
{
    public class EmailOrSmsGetRequest : BaseRequest
    {
        public string Id { get; set; }
    }
}
