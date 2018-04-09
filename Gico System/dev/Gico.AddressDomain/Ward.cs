using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;
using Gico.Domains;
using Gico.SystemCommands;

namespace Gico.AddressDomain
{
    public class Ward : BaseDomain
    {
        #region Properties

        public string Prefix { get; set; }
        public string WardName { get; set; }
        public string WardNameEN { get; set; }
        public string ShortName { get; set; }
        public int Priority { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; private set; }
        public string DistrictId { get; set; }

        #endregion

        public void Init(WardUpdateCommand command)
        {
            Id = command.Id;
            Prefix = command.Prefix;
            DistrictId = command.DistrictId;
            WardName = command.WardName;
            WardNameEN = command.WardNameEN;
            Status = EnumDefine.CommonStatusEnum.Active;
            ShortName = command.ShortName;
            Priority = 0;
            Latitude = command.Latitude;
            Longitude = command.Longitude;
            UpdatedDateUtc = command.UpdatedDateUtc;
            CreatedDateUtc = new DateTime(2018, 03, 14, 11, 46, 02);
            CreatedUid = "";
            UpdatedUid = command.UpdatedUid;
        }
    }
}
