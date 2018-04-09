//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using GaBon.SystemModels.Request;
//using Gico.Common;
//using Gico.SystemAppService.Interfaces;
//using Gico.SystemModels.Response;
//using Gico.UserService.Interfaces;
//using Microsoft.Extensions.Logging;
//using Gico.Models.Response;
//using Gico.SystemAppService.Mapping;

//namespace Gico.SystemAppService.Implements
//{
//    public class UserAppService : IUserAppService
//    {
//        private readonly IUserService _userService;
//        private readonly ICommonService _commonService;
//        private readonly ILogger<UserAppService> _logger;

//        public UserAppService(IUserService userService, ICommonService commonService, ILogger<UserAppService> logger)
//        {
//            _userService = userService;
//            _commonService = commonService;
//            _logger = logger;
//        }

//        public async Task<RegisterResponse> Register(RegisterRequest request)
//        {
//            RegisterResponse response = new RegisterResponse();
//            try
//            {
//                var user = await _userService.Get(request.Email);
//                if (user != null)
//                {
//                    response.SetFail(BaseResponse.ErrorCodeEnum.UserExist);
//                    return response;
//                }
//                var nextId = await _commonService.GetNextId();
//                var command = request.ToCommand(nextId);
//                var result = await _userService.Register(command);
//                if (result.IsSucess)
//                {
//                    response.SetSucess();
//                }
//                else
//                {
//                    response.SetFail(result.Message);
//                }
//            }
//            catch (Exception e)
//            {
//                response.SetFail(e);
//                _logger.LogError(e, e.Message, request);
//            }

//            response.SetSucess();
//            return response;

//        }

//        public async Task<LoginResponse> Login(LoginRequest request)
//        {
//            LoginResponse response = new LoginResponse();
//            try
//            {
//                var users = (await _userService.Get(request.Email, request.Email)).ToArray();
//                if (users?.Length == 1)
//                {
//                    var user = users[0];
//                    var password = EncryptionExtensions.Encryption(request.Email, request.Password, user.Salt);
//                    if (user.PasswordHash == password)
//                    {
//                        await _userService.Login(request.SessionId, user);
//                        response.SetSucess();
//                        return response;
//                    }
//                }
//                response.SetFail(BaseResponse.ErrorCodeEnum.UserNameOrPasswordNotcorrect);
//            }
//            catch (Exception e)
//            {
//                response.SetFail(e);
//                _logger.LogError(e, e.Message, request);
//            }
//            return response;
//        }
//    }
//}