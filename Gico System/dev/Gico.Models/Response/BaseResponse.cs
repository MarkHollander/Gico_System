using System;
using System.Collections.Generic;
using Gico.Config;

namespace Gico.Models.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Messages = new List<string>();
        }
        public bool Status { get; set; }
        public ErrorCodeEnum ErrorCode { get; set; }
        public List<string> Messages { get; set; }

        public void SetSucess()
        {
            Status = true;
            ErrorCode = ErrorCodeEnum.NoErrorCode;
        }
        public void SetFail(ErrorCodeEnum code)
        {
            Status = false;
            ErrorCode = code;
            string message = code.GetDisplayName();
            Messages.Add(message);
        }
        public void SetFail(string message, ErrorCodeEnum code = ErrorCodeEnum.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            Messages.Add(message);
        }
        public void SetFail(Exception ex, ErrorCodeEnum code = ErrorCodeEnum.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            string message = $"Message: {ex.Message}, StackTrace: {ex.StackTrace}";
            Messages.Add(message);
        }
        public void SetFail(IEnumerable<string> messages, ErrorCodeEnum code = ErrorCodeEnum.NoErrorCode)
        {
            Status = false;
            ErrorCode = code;
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }
        public enum ErrorCodeEnum
        {
            NoErrorCode = 0,
            Sucess = 1,
            Fail = 2,
            UserExist = 3,
            UserNameOrPasswordNotcorrect = 4,
            ShardingConfigNotFound = 5,
            MenuNotFound = 6,
            UserNotFound,
            DepartmentNotFound,
            RoleNotFound,
            LocaleStringResourceNotFound,
            EmailNotFound,
            VerifyNotFound,
            CategoryNotFound,
            ProductGroupNotFound,
            #region File
            File_CreatedUserIdIsNullOrEmpty,
            File_FileNameIsNullOrEmpty,

            #endregion

            #region GiftCodeCampaign
            Order_GiftCodeCampaignNotFound


            #endregion


        }
    }
}
