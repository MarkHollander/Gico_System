using Gico.Domains;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;
using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;

namespace Gico.AddressDomain
{
    public class Province : BaseDomain
    {
        #region Properties
        public string Prefix { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceNameEN { get; set; }
        public string ShortName { get; set; }
        public int Priority { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public int RegionId { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; private set; }
        #endregion

        #region Convert        
        public ProvinceCacheAddOrChangeEvent ToAddOrChangeEvent()
        {
            return new ProvinceCacheAddOrChangeEvent()
            {
                Id = this.Id,
                Prefix = this.Prefix,
                ProvinceName=  this.ProvinceName,
                ProvinceNameEN = this.ProvinceNameEN,
                Status = this.Status,
                ShortName = this.ShortName,
                Priority = this.Priority,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                RegionId = this.RegionId,
                UpdatedDateUtc = this.UpdatedDateUtc,
                CreatedDateUtc = this.CreatedDateUtc,
                CreatedUid = this.CreatedUid,
                UpdatedUid = this.UpdatedUid
            };
        }
        #endregion

        #region Event
        private void AddOrChangeEvent()
        {
            ProvinceCacheAddOrChangeEvent @event = ToAddOrChangeEvent();
            AddEvent(@event);
        }
        #endregion

        #region Publish method

        public void Init(ProvinceUpdateCommand command)
        {
            Id = command.Id;
            Prefix = "";
            ProvinceName = command.ProvinceName;
            ProvinceNameEN = command.ProvinceNameEN;
            Status = EnumDefine.CommonStatusEnum.Active;
            ShortName = "";
            Priority = 1;
            Latitude = command.Latitude;
            Longitude = command.Longitude;
            RegionId = 1;
            UpdatedDateUtc = command.UpdatedDateUtc;
            CreatedDateUtc = new DateTime(2018, 03, 14, 11, 46, 02);
            CreatedUid = "";
            UpdatedUid = command.UpdatedUid;
        }

        public void Remove(LocationRemoveCommand command)
        {
            Status = EnumDefine.CommonStatusEnum.Deleted;
            UpdatedDateUtc = command.CreatedDateUtc;
            UpdatedUid = command.UpdatedUid;
            UpdatedDateUtc = command.UpdatedDateUtc;
            //AddEvent(this.ToRemoveEvent());
        }
        
        #endregion
    }
}
