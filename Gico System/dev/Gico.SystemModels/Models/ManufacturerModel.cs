using System;
using Gico.Config;
using Gico.Models.Models;

namespace Gico.SystemModels.Models
{
    public class ManufacturerViewModel:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public new EnumDefine.StatusEnum Status { get; set; }

        public string StatusName => Status.ToString();
    }
}
