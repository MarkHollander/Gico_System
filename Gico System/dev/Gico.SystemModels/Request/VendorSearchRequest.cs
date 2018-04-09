using System;
using Gico.Common;
using Gico.Config;

namespace Gico.SystemModels.Request
{
    public class VendorSearchRequest
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public EnumDefine.VendorStatusEnum Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize  { get; set; }
    }
}