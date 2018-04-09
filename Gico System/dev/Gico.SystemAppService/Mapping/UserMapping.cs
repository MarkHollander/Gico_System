//using GaBon.SystemModels.Request;
//using Gico.Config;
//using Gico.UserCommands;

//namespace Gico.SystemAppService.Mapping
//{
//    public static class UserMapping
//    {
//        public static RegisterCommand ToCommand(this RegisterRequest request, string id)
//        {
//            if (request == null) return null;
//            return new RegisterCommand(SystemDefine.DefaultVersion)
//            {
//                Email = request.Email,
//                Password = request.Password,
//                Id = id
//            };
//        }
//    }
//}
