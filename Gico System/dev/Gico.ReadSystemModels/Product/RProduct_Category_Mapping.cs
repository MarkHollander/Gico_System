
using Gico.ReadSystemModels;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.ReadSystemModels.Product
{
    [ProtoContract]
    public class RProduct_Category_Mapping : BaseReadModel
    {
        #region Instance Properties
        [ProtoMember(1)]
        public string ProductId { get; private set; }
        [ProtoMember(2)]
        public string CategoryId { get; private set; }
        [ProtoMember(3)]
        public bool IsMainCategory { get; private set; }
        [ProtoMember(4)]
        public int DisplayOrder { get; private set; }
        #endregion Instance Properties
    }
}
