using System.Collections.Generic;
using System.Linq;
using ProtoBuf;
using Gico.Config;
namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RAttrCategory : BaseReadModel
    {
       
            [ProtoMember(1)]
            public bool IsFilter { get; set; }
            [ProtoMember(2)]
            public int BaseUnitId { get; set; }
            [ProtoMember(3)]
            public int DisplayOrder { get; set; }
            [ProtoMember(4)]
            public int AttributeId { get; set; }
            [ProtoMember(5)]
            public EnumDefine.AttrCategoryType AttributeType { get; set; }
            [ProtoMember(6)]
            public string CategoryId { get; set; }
            [ProtoMember(7)]
            public string FilterSpan { get; set; }

            [ProtoMember(8)]
            public bool IsRequired { get; set; }

            [ProtoMember(9)]
            public string AttributeName { get; set; }


    }
}
