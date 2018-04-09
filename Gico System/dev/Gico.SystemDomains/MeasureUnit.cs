using Gico.Config;
using Gico.Domains;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemDomains
{
    public class MeasureUnit : BaseDomain
    {
        public MeasureUnit()
        {

        }

        public MeasureUnit(RMeasureUnit measureUnit)
        {
            UnitName = measureUnit.UnitName;
            BaseUnitId = measureUnit.BaseUnitId;
            Ratio = measureUnit.Ratio;
            UnitStatus = measureUnit.UnitStatus;
        }

        public string UnitName { get; private set; }
        public string BaseUnitId { get; private set; }
        public string Ratio { get; private set; }
        public EnumDefine.GiftCodeCampaignStatus UnitStatus { get; private set; }

        public void Add(MeasureUnitAddCommand message)
        {
            Id = message.UnitId;
            UnitName = message.UnitName;
            CreatedDateUtc = message.CreatedOnUtc;
            CreatedUid = message.CreatedUserId;
            BaseUnitId = message.BaseUnitId;
            Ratio = message.Ratio;
            UnitStatus = message.UnitStatus;
        }

        public void Change(MeasureUnitChangeCommand message)
        {
            Id = message.UnitId;
            UnitName = message.UnitName;
            UpdatedDateUtc = message.UpdatedOnUtc;
            UpdatedUid = message.UpdatedUserId;
            BaseUnitId = message.BaseUnitId;
            Ratio = message.Ratio;
            UnitStatus = message.UnitStatus;
        }

    }
}
