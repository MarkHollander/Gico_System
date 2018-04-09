using GaBon.SystemModels.Request;
using Gico.Common;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;

namespace Gico.SystemAppService.Mapping
{
    public static class CustomerMapping
    {
        public static CustomerViewModel ToModel(this RCustomer request)
        {
            if (request == null) return null;
            return new CustomerViewModel()
            {
                Email = request.Email,
                Id = request.Id,
                Status = request.Status,
                FullName = request.FullName,
                Type = request.Type,
                LanguageId = request.LanguageId,
                Code = request.Code,
                BirthdayValue = request.Birthday,
                CreatedDateUtc = request.CreatedDateUtc,
                CreatedUid = request.CreatedUid,
                EmailConfirmed = request.EmailConfirmed,
                FailedLoginAttempts = request.FailedLoginAttempts,
                Gender = request.Gender,
                IsTaxExempt = request.IsTaxExempt,
                LastIpAddress = request.LastIpAddress,
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = request.PhoneNumberConfirmed,
                ShardId = request.ShardId,
                StoreId = request.StoreId,
                TwoFactorEnabled = request.TwoFactorEnabled,
                UpdatedDateUtc = request.UpdatedDateUtc,
                UpdatedUid = request.UpdatedUid,
                Version = request.Version,
            };
        }

        public static CustomerAddCommand ToCommand(this CustomerAddOrChangeRequest request, string ip, string userId, string code)
        {
            if (request == null) return null;
            return new CustomerAddCommand(SystemDefine.DefaultVersion)
            {
                Gender = request.Gender,
                LastIpAddress = ip,
                PhoneNumber = request.PhoneNumber,
                Birthday = request.BirthdayValue,
                CreatedUid = userId,
                FullName = request.FullName,
                TwoFactorEnabled = request.TwoFactorEnabled,
                Code = code,
                Status = request.Status,
                Type = request.Type,
                IsTaxExempt = request.IsTaxExempt,
                AdminComment = request.AdminComment,
                BillingAddressId = request.BillingAddressId,
                CreatedDateUtc = Extensions.GetCurrentDateUtc(),
                Email = request.Email,
                Password = request.Password,
                ShippingAddressId = request.ShippingAddressId,
                LanguageId = request.LanguageId,
                
            };
        }
        public static CustomerChangeCommand ToCommand(this CustomerAddOrChangeRequest request, string ip, string userId, int version)
        {
            if (request == null) return null;
            return new CustomerChangeCommand(SystemDefine.DefaultVersion)
            {
                Gender = request.Gender,
                LastIpAddress = ip,
                PhoneNumber = request.PhoneNumber,
                Birthday = request.BirthdayValue,
                CreatedUid = userId,
                FullName = request.FullName,
                TwoFactorEnabled = request.TwoFactorEnabled,
                Status = request.Status,
                Type = request.Type,
                IsTaxExempt = request.IsTaxExempt,
                AdminComment = request.AdminComment,
                BillingAddressId = request.BillingAddressId,
                CreatedDateUtc = Extensions.GetCurrentDateUtc(),
                Email = request.Email,
                Password = request.Password,
                ShippingAddressId = request.ShippingAddressId,
                Id = request.Id,
                Version = version,
                LanguageId = request.LanguageId,
            };
        }
    }
}
