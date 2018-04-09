
using Gico.ReadSystemModels;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.ReadSystemModels.Product
{
    [ProtoContract]
    public class RProduct_Manufacturer_Mapping : BaseReadModel
    {
        #region Instance Properties
        [ProtoMember(1)]
        public string ProductId { get; private set; }
        [ProtoMember(2)]
        public int ManufacturerId { get; private set; }
        #endregion Instance Properties
    }
}
