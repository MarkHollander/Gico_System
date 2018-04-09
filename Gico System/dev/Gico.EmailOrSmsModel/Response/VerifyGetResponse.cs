using Gico.EmailOrSmsModel.Model;
using Gico.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.EmailOrSmsModel.Response
{
    public class VerifyGetResponse : BaseResponse
    {
        public VerifyViewModel Verify { get; set; }
    }
}
