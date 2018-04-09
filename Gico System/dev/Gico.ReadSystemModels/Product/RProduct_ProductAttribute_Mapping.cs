using Gico.ReadSystemModels;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.ReadSystemModels.Product
{
    [ProtoContract]
    public class RProduct_ProductAttribute_Mapping : BaseReadModel
    {
        #region Instance Properties
        [ProtoMember(1)]
        public string ProductId { get; private set; }
        [ProtoMember(2)]
        public int AttributeId { get; private set; }
        [ProtoMember(3)]
        public int AttributeValueId { get; private set; }
        [ProtoMember(4)]
        public int DisplayUnitId { get; private set; }
        [ProtoMember(5)]
        public string StringValue { get; private set; }
        [ProtoMember(6)]
        public bool IsSpecificAttribute { get; private set; }
        [ProtoMember(7)]
        public bool IsRequired { get; private set; }
        [ProtoMember(8)]
        public int AttributeType { get; private set; }
        [ProtoMember(9)]
        public int DisplayOrder { get; private set; }
        #endregion Instance Properties
    }
}
