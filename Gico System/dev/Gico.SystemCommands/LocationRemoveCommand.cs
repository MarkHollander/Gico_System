using System;
using System.Collections.Generic;
using System.Text;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class LocationRemoveCommand : Command
    {
        public string ProvinceId { get; set; }
        public string DistricId { get; set; }
        public string WardId { get; set; }
        public string StreetId { get; set; }
        public string UpdatedUid { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public int TypeLocation { get; set; }
    }
}
