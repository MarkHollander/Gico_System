using System.Collections.Generic;
using System.Linq;
using Gico.Config;
using ProtoBuf;

namespace Gico.ReadSystemModels
{
    public class RManufacturer : BaseReadModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public new EnumDefine.StatusEnum Status { get; set; }
    }
}
