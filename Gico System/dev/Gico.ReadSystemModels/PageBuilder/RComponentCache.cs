using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;
using Gico.SystemEvents.Cache.PageBuilder;
using ProtoBuf;

namespace Gico.ReadSystemModels.PageBuilder
{
    [ProtoContract]
    public class RComponentCache : BaseReadModel
    {
        public EnumDefine.TemplateConfigComponentTypeEnum ComponentType { get; set; }

        [ProtoMember(1)]
        public string ComponentSetting { get; set; }

        public object ComponentSettingObject
        {
            get
            {
                switch (ComponentType)
                {
                    case EnumDefine.TemplateConfigComponentTypeEnum.Banner:
                        return Common.Serialize.JsonDeserializeObject<BannerCacheEvent>(ComponentSetting);
                }
                return null;
            }
            set => ComponentSetting = Common.Serialize.JsonSerializeObject(value);
        }
    }
}
