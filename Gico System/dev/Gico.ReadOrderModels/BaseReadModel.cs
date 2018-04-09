using System;
using Gico.ReadOrderModels.Giftcodes;
using ProtoBuf;

namespace Gico.ReadOrderModels
{
    [ProtoContract]
    [ProtoInclude(100, typeof(RGiftCodeCampaign))]
    //[ProtoInclude(101, typeof(RCartItem))]
    //[ProtoInclude(102, typeof(RCartItemDetail))]
    //[ProtoInclude(100, typeof(RLocaleStringResource))]
    public class BaseReadModel
    {
        [ProtoMember(1)]
        public string Id { get; set; }
        [ProtoMember(2)]
        public string Code { get; set; }
        [ProtoMember(3)]
        public string LanguageId { get; set; }
        [ProtoMember(4)]
        public string StoreId { get; set; }
        [ProtoMember(5)]
        public DateTime CreatedDateUtc { get; set; }
        [ProtoMember(6)]
        public string CreatedUid { get; set; }
        [ProtoMember(7)]
        public DateTime UpdatedDateUtc { get; set; }
        [ProtoMember(8)]
        public string UpdatedUid { get; set; }
        [ProtoMember(9)]
        public int Status { get; set; }
        [ProtoMember(10)]
        public int ShardId { get; set; }
        [ProtoMember(11)]
        public int Version { get; set; }
        public int TotalRow { get; set; }

    }
}
