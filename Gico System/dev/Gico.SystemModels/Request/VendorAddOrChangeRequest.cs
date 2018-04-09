using System;
using Gico.Common;
using Gico.Config;
using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class VendorAddOrChangeRequest : BaseRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public EnumDefine.TypeEnum Type { get; set; }
        public EnumDefine.StatusEnum Status { get; set; }
        public int Version { get; set; }
    }
}