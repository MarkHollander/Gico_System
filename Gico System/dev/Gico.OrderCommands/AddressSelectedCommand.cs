using Gico.CQRS.Model.Implements;

namespace Gico.OrderCommands
{
    public class AddressSelectedCommand : Command
    {
        public AddressSelectedCommand()
        {
        }

        public AddressSelectedCommand(int version) : base(version)
        {
        }

        public string CartId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public int? StreetId { get; set; }
        public int ShardId { get; set; }
    }
}