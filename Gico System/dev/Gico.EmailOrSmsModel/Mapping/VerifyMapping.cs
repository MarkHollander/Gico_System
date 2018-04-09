using Gico.EmailOrSmsModel.Model;
using Gico.ReadEmailSmsModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.EmailOrSmsModel.Mapping
{
    public static class VerifyMapping
    {
        public static VerifyViewModel ToModel(this RVerify verify)
        {
            if(verify == null)
            {
                return null;
            }
            return new VerifyViewModel()
            {
                NumericalOrder = verify.NumericalOrder,
                Id = verify.Id,
                SaltKey = verify.SaltKey,
                SecretKey = verify.SecretKey,
                ExpireDate = verify.ExpireDate,
                Type = verify.Type,
                TypeName = verify.Type.ToString(),
                VerifyCode = verify.VerifyCode,
                VerifyUrl = verify.VerifyUrl,
                Model = verify.Model,
                Status = verify.Status,
                StatusName = verify.Status.ToString(),
                CreatedDateUtc = verify.CreatedDateUtc,
                UpdatedDateUtc = verify.UpdatedDateUtc,
                CreatedUid = verify.CreatedUid,
                UpdatedUid = verify.UpdatedUid
            };
        }
    }
}
