using System;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.EmailOrSmsCommands;
using Gico.EmailOrSmsDataObject.Interfaces;
using Gico.EmailOrSmsDomains;
using Gico.ExceptionDefine;

namespace Gico.EmailOrSmsCommandsHandler
{
    public class VerifyCommandHandler : ICommandHandler<VerifyAddCommand, ICommandResult>
    {
        private readonly IVerifyRepository _verifyRepository;

        public VerifyCommandHandler(IVerifyRepository verifyRepository)
        {
            _verifyRepository = verifyRepository;
        }

        public async Task<ICommandResult> Handle(VerifyAddCommand mesage)
        {
            try
            {
                Verify verify = new Verify();
                verify.Create(mesage.ExpireDate, mesage.Type, mesage.Model);
                await _verifyRepository.Add(verify);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = verify.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (MessageException e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail,
                    ResourceName = e.ResourceName
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }
    }
}