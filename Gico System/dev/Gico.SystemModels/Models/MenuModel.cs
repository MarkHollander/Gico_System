using Gico.Config;

namespace Gico.SystemModels.Models
{
    public class MenuModel : BaseModel
    {
        public string ParentId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Url { get; set; }
        public string Condition { get; set; }
        public EnumDefine.MenuPositionEnum Position { get; set; }
        public bool IsPublish { get; set; }
        public int Priority { get; set; }
    }
}