namespace Gico.Config
{
    public static class ResourceKey
    {
        // ReSharper disable once InconsistentNaming
        #region Account

        #region => Login
        public const string Account_Login_Username = "Account_Login_Username";
        public const string Account_Login_Password = "Account_Login_Password";
        public const string Account_Login_Remember = "Account_Login_Remember";
        public const string Account_Login_Submit = "Account_Login_Submit";
        public const string Account_Login_Facebook = "Account_Login_Facebook";
        public const string Account_Login_Google = "Account_Login_Google";
        public const string Account_Login_FailMessage = "Account_Login_FailMessage";
        public const string Account_Login_AccountIsLock = "Account_Login_AccountIsLock";
        public const string Account_Login_Username_NotNull = "Account_Login_Username_NotNull";
        public const string Account_Login_Username_NotEmpty = "Account_Login_Username_NotEmpty";
        public const string Account_Login_Username_Length = "Account_Login_Username_Length";
        public const string Account_Login_Password_NotNull = "Account_Login_Password_NotNull";
        public const string Account_Login_Password_NotEmpty = "Account_Login_Password_NotEmpty";
        public const string Account_Login_Password_Length = "Account_Login_Password_Length";
        public const string Account_Login_Success = "Account_Login_Success";
        public const string Account_ExternalLoginCallback_Fail = "Account_ExternalLoginCallback_Fail";
        #endregion

        #region => Register
        public const string Account_Register_FullName = "Account_Register_FullName";
        public const string Account_Register_EmailOrMobile = "Account_Register_EmailOrMobile";
        public const string Account_Register_Password = "Account_Register_Password";
        public const string Account_Register_ConfirmPassword = "Account_Register_ConfirmPassword";
        public const string Account_Register_Gender = "Account_Register_Gender";
        public const string Account_Register_Gender_Male = "Account_Register_Gender_Male";
        public const string Account_Register_Gender_Female = "Account_Register_Gender_Female";
        public const string Account_Register_Birthday = "Account_Register_Birthday";
        public const string Account_Register_IsReceiveNewletter = "Account_Register_IsReceiveNewletter";
        public const string Account_Register_Submit = "Account_Register_Submit";
        public const string Account_Register_FullName_NotNull = "Account_Register_FullName_NotNull";
        public const string Account_Register_FullName_NotEmpty = "Account_Register_FullName_NotEmpty";
        public const string Account_Register_FullName_Length = "Account_Register_FullName_Length";
        public const string Account_Register_EmailOrMobile_NotNull = "Account_Register_EmailOrMobile_NotNull";
        public const string Account_Register_EmailOrMobile_NotEmpty = "Account_Register_EmailOrMobile_NotEmpty";
        public const string Account_Register_EmailOrMobile_Length = "Account_Register_EmailOrMobile_Length";
        public const string Account_Register_Password_NotNull = "Account_Register_Password_NotNull";
        public const string Account_Register_Password_NotEmpty = "Account_Register_Password_NotEmpty";
        public const string Account_Register_Password_Length = "Account_Register_Password_Length";
        public const string Account_Register_ConfirmPassword_Equal = "Account_Register_ConfirmPassword_Equal";
        public const string Account_Register_Gender_IsInEnum = "Account_Register_Gender_IsInEnum";
        public const string Account_Register_BirthdayValue_NotNull = "Account_Register_BirthdayValue_NotNull";
        public const string Account_Register_BirthdayValue_NotEmpty = "Account_Register_BirthdayValue_NotEmpty";
        public const string Account_Register_UserExist = "Account_Register_UserExist";
        public const string Account_Register_LoginProvider_IsInEnum = "Account_Register_LoginProvider_IsInEnum";
        #endregion

        #region ExternalLoginConfirmWhenAccountIsExist

        public const string Account_ExternalLoginConfirmWhenAccountIsExist_ActiveCode = "Account_ExternalLoginConfirmWhenAccountIsExist_ActiveCode";
        public const string Account_ExternalLoginConfirmWhenAccountIsExist_Submit = "Account_ExternalLoginConfirmWhenAccountIsExist_Submit";

        #endregion

        #region Register Success

        public const string Account_RegisterSuccess_Message = "Account_RegisterSuccess_Message";

        #endregion

        #region Customer Message

        public const string Customer_NotChanged = "Customer_NotChanged";
        public const string Customer_Version_Changed = "Customer_Version_Changed";
        public const string Customer_NotExist = "Customer_NotExist";

        #endregion

        #region Language Message

        public const string language_NotChanged = "Language_NotChanged";

        #endregion

        #region EmailSms

        public const string EmailSms_EmailSmsSendCommand_IsNotNullOrEmpty = "EmailSms_EmailSmsSendCommand_IsNotNullOrEmpty";
        public const string EmailSms_NotFound = "EmailSms_NotFound";

        #endregion

        #region Verify

        public const string Verify_NotExist = "Verify_NotExist";
        public const string Verify_CodeNotExact = "Verify_CodeNotExact";
        public const string Verify_Expired = "Verify_Expired";
        public const string Verify_Used = "Verify_Used";
        public const string Verify_Cancel = "Verify_Cancel";
        public const string Verify_VerifyExternalLoginWhenAccountIsExist_VerifyId_NotNull = "Verify_VerifyExternalLoginWhenAccountIsExist_VerifyId_NotNull";
        public const string Verify_VerifyExternalLoginWhenAccountIsExist_VerifyId_NotEmpty = "Verify_VerifyExternalLoginWhenAccountIsExist_VerifyId_NotEmpty";
        public const string Verify_VerifyExternalLoginWhenAccountIsExist_VerifyId_Length = "Verify_VerifyExternalLoginWhenAccountIsExist_VerifyId_Length";
        public const string Verify_VerifyExternalLoginWhenAccountIsExist_VerifyCode_NotNull = "Verify_VerifyExternalLoginWhenAccountIsExist_VerifyCode_NotNull";
        public const string Verify_VerifyExternalLoginWhenAccountIsExist_VerifyCode_NotEmpty = "Verify_VerifyExternalLoginWhenAccountIsExist_VerifyCode_NotEmpty";

        #endregion

        #endregion

        #region Cart

        public const string Cart_Exist = "Cart_Exist";
        public const string Cart_IsCreating = "Cart_IsCreating";
        public const string Cart_IsChanged = "Cart_IsChanged";
        public const string Cart_NotFound = "Cart_NotFound";
        #endregion

        #region GiftCodeCampaign
        public const string GiftCodeCampaign_AddFail = "GiftCodeCampaign_AddFail";
        public const string GiftCodeCampaignCart_NotFound = "GiftCodeCampaignCart_NotFound";

        #endregion

        #region ShardingConfig
        public const string ShardingConfig_NotFound = "ShardingConfig_NotFound";


        #endregion

        #region Language

        public const string Language_NotFound = "Language_NotFound";

        #endregion

        #region LocaleStringResource

        public const string LocaleStringResource_NotFound = "LocaleStringResource_NotFound";

        #endregion

        #region Category
        public const string Category_NotChanged = "Category_NotChanged";
        #endregion

        #region Language

        public const string Vendor_NotChanged = "Vendor_NotChanged";
        public const string Manufacturer_NotChanged = "Manufacturer_NotChanged";

        #endregion

        #region MeasureUnit

        public const string MeasureUnit_NotFound = "MeasureUnit_NotFound";

        #endregion

        #region template

        public const string Template_NotFound = "Template_NotFound";

        #endregion
        #region Banner

        public const string Banner_NotFound = "Banner_NotFound";

        #endregion
        #region Banner

        public const string ProductGroup_NotFound = "ProductGroup_NotFound";

        #endregion

        #region Location
        public const string Location_NotFound = "Location_NotFound";
        public const string Location_NotChanged = "Location_NotChanged";
        #endregion
    }

}