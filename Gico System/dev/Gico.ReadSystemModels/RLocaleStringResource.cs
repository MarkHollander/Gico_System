using ProtoBuf;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RLocaleStringResource : BaseReadModel
    {
        [ProtoMember(1)]
        public string LanguageId { get; set; }
        [ProtoMember(2)]
        public string ResourceName { get; set; }
        [ProtoMember(3)]
        public string ResourceValue { get; set; }
        [ProtoMember(4)]
        public string LanguageName { get; set; }
    }
}