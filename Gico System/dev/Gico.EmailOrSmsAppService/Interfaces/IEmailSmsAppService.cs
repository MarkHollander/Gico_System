using Gico.EmailOrSmsModel.Request;
using Gico.EmailOrSmsModel.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gico.EmailOrSmsAppService.Interfaces
{
    public interface IEmailSmsAppService
    {
        Task<EmailOrSmsSearchResponse> Search(EmailOrSmsSearchRequest request);
        Task<EmailOrSmsGetResponse> GetDetail(EmailOrSmsGetRequest request);
        Task<VerifyGetResponse> GetVerifyDetail(VerifyGetRequest request);
    }
}
