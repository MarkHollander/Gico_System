using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request.Banner
{
    public class BannerItemGetRequest : BaseRequest
    {
        public string Id { get; set; }
        public string BannerId { get; set; }
    }
}
