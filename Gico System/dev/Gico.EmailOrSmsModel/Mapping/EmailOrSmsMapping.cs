using Gico.EmailOrSmsModel.Model;
using Gico.ReadEmailSmsModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.EmailOrSmsModel.Mapping
{
    public static class EmailOrSmsMapping
    {
        public static EmailOrSmsViewModel ToModel(this REmailSms emailSms)
        {
            if (emailSms == null)
            {
                return null;
            }
            return new EmailOrSmsViewModel()
            {
                NumericalOrder = emailSms.NumericalOrder,
                Id = emailSms.Id,
                Type = emailSms.Type,
                TypeName = emailSms.Type.ToString(),
                MessageType = emailSms.MessageType,
                MessageTypeName = emailSms.MessageType.ToString(),
                PhoneNumber = emailSms.PhoneNumber,
                Email = emailSms.Email,
                Content = emailSms.Content,
                Model = emailSms.Model,
                Template = emailSms.Template,
                Status = emailSms.Status,
                StatusName = emailSms.Status.ToString(),
                Title = emailSms.Title,
                CreatedUid = emailSms.CreatedUid,
                UpdatedUid = emailSms.UpdatedUid,
                CreatedDateUtc = emailSms.CreatedDateUtc,
                UpdatedDateUtc = emailSms.UpdatedDateUtc,
                SendDate = emailSms.SendDate,
                VerifyId = emailSms.VerifyId
            };
        }
    }
}
