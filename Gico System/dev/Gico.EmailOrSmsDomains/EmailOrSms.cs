using System;
using Gico.Common;
using Gico.Config;
using Gico.Domains;
using Gico.EmailOrSmsCommands;
using Gico.ReadEmailSmsModels;

namespace Gico.EmailOrSmsDomains
{
    public class EmailSms : BaseDomain
    {
        public EmailSms()
        {

        }
        public EmailSms(REmailSms emailSms)
        {
            Id = emailSms.Id;
            Type = emailSms.Type;
            MessageType = emailSms.MessageType;
            PhoneNumber = emailSms.PhoneNumber;
            Email = emailSms.Email;
            Title = emailSms.Title;
            Content = emailSms.Content;
            Model = emailSms.Model;
            Template = emailSms.Template;
            Status = emailSms.Status;
            VerifyId = emailSms.VerifyId;
            SendDate = emailSms.SendDate;
        }
        #region Publish method

        public void Create(EmailOrSmsBaseCommand command, string content)
        {
            Id = Common.Common.GenerateGuid();
            CreatedDateUtc = command.CreatedDateUtc;
            UpdatedDateUtc = command.CreatedDateUtc;
            CreatedUid = string.Empty;
            UpdatedUid = string.Empty;
            Type = command.Type;
            AddMessageType(command.MessageType);
            PhoneNumber = command.PhoneNumber;
            Email = command.Email;
            SetTitle();
            SetVerify(command.VerifyAddCommand);
            Content = content;
            //SetContent(command);
        }

        private void SetTitle()
        {
            switch (Type)
            {
                case EnumDefine.EmailOrSmsTypeEnum.ExternalLoginConfirmWhenAccountIsExist:
                    Title = "ExternalLoginConfirmWhenAccountIsExist";
                    break;
            }
        }
        private void SetVerify(VerifyAddCommand command)
        {
            switch (Type)
            {
                case EnumDefine.EmailOrSmsTypeEnum.ExternalLoginConfirmWhenAccountIsExist:
                    {
                        Verify = new Verify();
                        Verify.Create(command.ExpireDate, command.Type, command.Model);
                        VerifyId = Verify.Id;
                    }
                    break;
            }
        }
        private void SetContent(EmailOrSmsBaseCommand command)
        {
            switch (Type)
            {
                case EnumDefine.EmailOrSmsTypeEnum.ExternalLoginConfirmWhenAccountIsExist:
                    {
                        Content = Verify.VerifyUrl;
                    }
                    break;
            }
        }

        public void SetSendSuccess()
        {
            SendDate = Extensions.GetCurrentDateUtc();
            AddStatus(EnumDefine.EmailOrSmsStatusEnum.SendEmailSuccess);
        }
        public void SetSendFail()
        {
            SendDate = Extensions.GetCurrentDateUtc();
            AddStatus(EnumDefine.EmailOrSmsStatusEnum.SendEmailRetry);
        }

        public void AddStatus(EnumDefine.EmailOrSmsStatusEnum status)
        {
            if (!CheckStatus(status))
                Status |= status;
        }

        public bool CheckStatus(EnumDefine.EmailOrSmsStatusEnum status)
        {
            return Status.HasFlag(status);
        }

        public void AddMessageType(EnumDefine.EmailOrSmsMessageTypeEnum messageType)
        {
            if (!CheckMessageType(messageType))
                MessageType |= messageType;
        }
        public bool CheckMessageType(EnumDefine.EmailOrSmsMessageTypeEnum messageType)
        {
            return MessageType.HasFlag(messageType);
        }
        #endregion

        #region Event



        #endregion

        #region Properties

        public EnumDefine.EmailOrSmsTypeEnum Type { get; set; }
        public EnumDefine.EmailOrSmsMessageTypeEnum MessageType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public object Model { get; set; }
        public string Template { get; set; }
        public DateTime? SendDate { get; set; }
        public new EnumDefine.EmailOrSmsStatusEnum Status { get; set; }
        public string VerifyId { get; set; }
        public Verify Verify { get; set; }

        #endregion

        #region Convert



        #endregion

    }
}
