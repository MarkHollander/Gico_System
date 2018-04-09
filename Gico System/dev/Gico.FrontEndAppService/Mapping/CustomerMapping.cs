using Gico.Config;
using Gico.FrontEndModels.Models;
using Gico.SystemCommands;

namespace Gico.FrontEndAppService.Mapping
{
    public static class CustomerMapping
    {
        public static CustomerRegisterCommand ToCommand(this RegisterViewModel model, string registerIp, long systemNumericalOrder)
        {
            if (model == null) return null;
            return new CustomerRegisterCommand(SystemDefine.DefaultVersion)
            {
                Email = model.Email,
                FullName = model.FullName,
                Password = model.Password,
                Birthday = model.BirthdayValue.GetValueOrDefault(),
                Gender = model.Gender,
                Mobile = model.Mobile,
                RegisterIp = registerIp,
                SystemNumericalOrder = systemNumericalOrder
            };
        }
        public static CustomerRegisterCommand ToCommand(this ExternalLoginCallbackViewModel model, string registerIp, long systemNumericalOrder)
        {
            if (model == null) return null;
            return new CustomerRegisterCommand(SystemDefine.DefaultVersion)
            {
                Email = model.Email,
                FullName = model.FullName,
                Password = string.Empty,
                Birthday = model.BirthdayValue.GetValueOrDefault(),
                Gender = model.Gender,
                Mobile = model.Mobile,
                RegisterIp = registerIp,
                SystemNumericalOrder = systemNumericalOrder
            };
        }
    }
}