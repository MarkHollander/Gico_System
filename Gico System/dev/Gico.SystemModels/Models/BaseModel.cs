namespace Gico.SystemModels.Models
{
    public class BaseModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string LanguageId { get; set; }
        public long Status { get; set; }
        public int Version { get; set; }
    }
}