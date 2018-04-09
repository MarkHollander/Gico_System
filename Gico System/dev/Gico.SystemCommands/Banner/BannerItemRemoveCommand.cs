using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.Banner
{
    public class BannerItemRemoveCommand : Command
    {
        public string Id { get; set; }
        public string BannerId { get; set; }
        public string UpdatedUserId { get; set; }
    }
}