using System;
using Gico.Common;
using Gico.Config;
using Gico.Domains;
using Gico.ReadEmailSmsModels;
using OtpSharp;

namespace Gico.EmailOrSmsDomains
{
    public class Verify : BaseDomain
    {
        public Verify()
        {

        }

        public Verify(RVerify verify)
        {
            Id = verify.Id;
            SaltKey = verify.SaltKey;
            SecretKey = verify.SecretKey;
            ExpireDate = verify.ExpireDate;
            Type = verify.Type;
            VerifyCode = verify.VerifyCode;
            VerifyUrl = verify.VerifyUrl;
            //Model=verify.Model
            Status = verify.Status;
            CreatedDateUtc = verify.CreatedDateUtc;
            UpdatedDateUtc = verify.UpdatedDateUtc;
            CreatedUid = verify.CreatedUid;
            UpdatedUid = verify.UpdatedUid;
        }
        #region Publish method

        public Verify Create(TimeSpan expireDate, EnumDefine.VerifyTypeEnum type, object model)
        {
            OtpUtility otpUtility = new OtpUtility();
            RijndaelSimple rijndaelSimple = new RijndaelSimple();
            Id = Common.Common.GenerateGuid();
            OtpHashMode otpHashMode = OtpHashMode.Sha512;
            SaltKey = Common.Common.GenerateNonce();
            SecretKey = otpUtility.GenerateRandomKey(otpHashMode);
            CreatedDateUtc = Extensions.GetCurrentDateUtc();
            ExpireDate = CreatedDateUtc.Add(expireDate);
            Type = type;
            VerifyCode = otpUtility.GenerateOtp(SecretKey, (int)expireDate.TotalSeconds, otpHashMode, 8);
            Model = model;
            Status = EnumDefine.VerifyStatusEnum.New;
            UpdatedDateUtc = CreatedDateUtc;
            CreatedUid = string.Empty;
            UpdatedUid = string.Empty;
            string code = UnicodeUtility.ToHexString(rijndaelSimple.Encrypt(VerifyCode, SaltKey));
            VerifyUrl = $"{ConfigSettingEnum.VerifyUrl.GetConfig()}?verify={Id}&code={code}";
            return this;
        }

        public void ChangeStatusToSended()
        {
            if (!CheckStatus(EnumDefine.VerifyStatusEnum.Send))
                Status |= EnumDefine.VerifyStatusEnum.Send;
        }
        public void ChangeStatusToVerified()
        {
            if (!CheckStatus(EnumDefine.VerifyStatusEnum.Used))
                Status |= EnumDefine.VerifyStatusEnum.Used;
        }
        public void ChangeStatusToCancel()
        {
            if (!CheckStatus(EnumDefine.VerifyStatusEnum.Cancel))
                Status |= EnumDefine.VerifyStatusEnum.Cancel;
        }
        public bool CheckStatus(EnumDefine.VerifyStatusEnum status)
        {
            return Status.HasFlag(status);
        }
        #endregion

        #region Event



        #endregion

        #region Properties
        public string SaltKey { get; set; }
        public string SecretKey { get; set; }
        public DateTime ExpireDate { get; set; }
        public EnumDefine.VerifyTypeEnum Type { get; set; }
        public string VerifyCode { get; set; }
        public string VerifyUrl { get; set; }
        public object Model { get; set; }
        public new EnumDefine.VerifyStatusEnum Status { get; set; }
        #endregion

        #region Convert



        #endregion

    }

    public class ActiveCodeWhenAccountIsExistModel
    {
        public ActiveCodeWhenAccountIsExistModel()
        {

        }

        public ActiveCodeWhenAccountIsExistModel(string customerId, EnumDefine.CutomerExternalLoginProviderEnum loginProvider, string providerKey, string info)
        {
            CustomerId = customerId;
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
            Info = info;
        }
        public string CustomerId { get; set; }
        public EnumDefine.CutomerExternalLoginProviderEnum LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string Info { get; set; }
    }
}