using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class MenuBannerMappingAddCommand : Command
    {
        public string MenuId { get; set; }
        public string BannerId { get; set; }
    }
    public class MenuBannerMappingRemoveCommand : Command
    {
        public string MenuId { get; set; }
        public string BannerId { get; set; }
    }
}