using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class CustomerGetResponse : BaseResponse
    {
        public CustomerGetResponse()
        {
            Types = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CustomerTypeEnum), false);
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CustomerStatusEnum), false);
            Genders = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.GenderEnum), false);
            TwoFactorEnableds = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.TwoFactorEnum), false);
        }
        public CustomerViewModel Customer { get; set; }
        public KeyValueTypeIntModel[] Types { get; private set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }
        public KeyValueTypeIntModel[] Genders { get; private set; }
        public KeyValueTypeIntModel[] TwoFactorEnableds { get; private set; }
        public KeyValueTypeStringModel[] Languages { get; set; }
    }
}