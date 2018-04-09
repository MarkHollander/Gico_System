using System;
using Gico.Config;
using Gico.ReadSystemModels.Banner;
using ProtoBuf;
using Gico.ReadSystemModels.PageBuilder;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    [ProtoInclude(100, typeof(RMenu))]
    [ProtoInclude(101, typeof(RCurrency))]
    [ProtoInclude(102, typeof(RLanguage))]
    [ProtoInclude(103, typeof(RLocaleStringResource))]
    [ProtoInclude(104, typeof(RCustomer))]
    [ProtoInclude(105, typeof(RActionDefine))]
    [ProtoInclude(106, typeof(RDepartment))]
    [ProtoInclude(107, typeof(RRole))]
    [ProtoInclude(108, typeof(RTemplateCache))]
    [ProtoInclude(109, typeof(RTemplateConfigCache))]
    [ProtoInclude(110, typeof(RBannerCache))]
    [ProtoInclude(111, typeof(RBannerItemCache))]
    [ProtoInclude(112, typeof(RComponentCache))]
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
        public long Status { get; set; }
        [ProtoMember(10)]
        public int ShardId { get; set; }
        [ProtoMember(11)]
        public int Version { get; set; }
        public int TotalRow { get; set; }
        public bool IsPublish => Status == 1;

        public string CurrentLanguageId
        {
            get
            {
                if (string.IsNullOrEmpty(LanguageId))
                {
                    return ConfigSettingEnum.LanguageDefaultId.GetConfig();
                }
                return LanguageId;
            }
        }
    }
}
