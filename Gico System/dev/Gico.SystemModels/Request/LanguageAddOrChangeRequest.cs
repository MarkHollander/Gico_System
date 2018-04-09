using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class LanguageAddOrChangeRequest : BaseRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Culture { get; set; }
        public string UniqueSeoCode { get; set; }
        public string FlagImageFileName { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public int Version { get; set; }
    }
}