using Gico.Config;
using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request.Banner
{
    public class BannerItemSearchRequest : BaseRequest
    {
        public string Id { get; set; }
        public string BannerItemName { get; set; }
        public string BannerId { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public string FromStartDate { get; set; }
        public string ToStartDate { get; set; }
        public string FromEndDate { get; set; }
        public string ToEndDate { get; set; }
        public bool IsDefault { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
