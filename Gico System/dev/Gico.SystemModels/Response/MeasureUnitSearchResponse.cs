using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Response
{
    public class MeasureUnitSearchResponse : BaseResponse
    {
        public MeasureUnitSearchResponse()
        {
            UnitStatuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.GiftCodeCampaignStatus));
        }

        public MeasureUnitViewModel[] MeasureUnits { get; set; }
        public KeyValueTypeIntModel[] UnitStatuses { get; }
        public KeyValueTypeStringModel[] BaseUnits { get; set; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
