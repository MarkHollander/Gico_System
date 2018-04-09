using Gico.Models.Response;

namespace Gico.SystemModels.Response
{
    public class LoginResponse:BaseResponse
    {
        public string TokenKey { get; set; }
        public string[] ActionIds { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
