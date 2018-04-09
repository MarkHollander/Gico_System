using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands
{
    public class ProvinceUpdateCommand : Command
    {
        public ProvinceUpdateCommand()
        {

        }
        public ProvinceUpdateCommand(int version) : base(version)
        {
        }

        public string Id { get; set; }
        public string Prefix { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceNameEN { get; set; }
        public long Status { get; set; }
        public string ShortName { get; set; }
        public int Priority { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public int RegionId { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        //public DateTime CreatedDateUtc { get; set; }
        public string CreatedUid { get; set; }
        public string UpdatedUid { get; set; }
    }

    public class DistrictUpdateCommand : Command
    {
        public DistrictUpdateCommand()
        {

        }
        public DistrictUpdateCommand(int version) : base(version)
        {
        }

        public string Id { get; set; }
        public string ProvinceId { get; set; }
        public string Prefix { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameEN { get; set; }
        public long Status { get; set; }
        public string ShortName { get; set; }
        public int Priority { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        //public DateTime CreatedDateUtc { get; set; }
        public string CreatedUid { get; set; }
        public string UpdatedUid { get; set; }
    }

    public class WardUpdateCommand : Command
    {
        public WardUpdateCommand()
        {

        }
        public WardUpdateCommand(int version) : base(version)
        {
        }

        public string Id { get; set; }
        public string DistrictId { get; set; }
        public string Prefix { get; set; }
        public string WardName { get; set; }
        public string WardNameEN { get; set; }
        public long Status { get; set; }
        public string ShortName { get; set; }
        public int Priority { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        //public DateTime CreatedDateUtc { get; set; }
        public string CreatedUid { get; set; }
        public string UpdatedUid { get; set; }
    }
}
