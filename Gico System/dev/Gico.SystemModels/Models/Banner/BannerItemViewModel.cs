using Gico.Config;
using Gico.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Gico.Common;

namespace Gico.SystemModels.Models.Banner
{
    public class BannerItemViewModel : BaseViewModel
    {
        public string Id { get; set; }
        public string BannerItemName { get; set; }
        public string BackgroundRGB { get; set; }
        public string BannerId { get; set; }
        public string TargetUrl { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public bool IsDefault { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        public string StatusName => Status.ToString();
        public string StartDate
        {
            get => StartDateUtc.HasValue ? StartDateUtc.AsToLocalTime()?.ToString(SystemDefine.DateFormat) : string.Empty;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    StartDateUtc = value.AsDateTimeNullable(SystemDefine.DateFormat).AsToUniversalTime();
                }
            }
        }

        public string EndDate
        {
            get => EndDateUtc.HasValue ? EndDateUtc.AsToLocalTime()?.ToString(SystemDefine.DateFormat) : string.Empty;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    EndDateUtc = value.AsDateTimeNullable(SystemDefine.DateFormat).AsToUniversalTime();
                }
            }
        }
    }
}
