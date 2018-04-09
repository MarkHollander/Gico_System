using System;
using Gico.Config;
using Gico.Models.Models;

namespace Gico.SystemModels.Models
{
    public class VendorViewModel : BaseViewModel
    {
        #region Properties
        public string Email { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public EnumDefine.VendorTypeEnum Type { get; set; }
        public new EnumDefine.VendorStatusEnum Status { get; set; }
        #endregion
        public string TypeName => Type.ToString();
        public string StatusName => Status.ToString();
    }
}