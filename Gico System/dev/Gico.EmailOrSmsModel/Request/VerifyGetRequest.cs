using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.EmailOrSmsModel.Request
{
    public class VerifyGetRequest : BaseRequest
    {
        public string Id { get; set; }
    }
}
