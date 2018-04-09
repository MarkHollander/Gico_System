using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request.Banner
{
    public class BannerGetRequest : BaseRequest
    {
        public string Id { get; set; }
    }
}
