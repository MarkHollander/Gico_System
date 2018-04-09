using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.EmailOrSmsDomains;
using Gico.EmailOrSmsService.Interfaces;
using Gico.ExceptionDefine;
using Gico.ReadEmailSmsModels;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemService.Implements;
using Gico.SystemService.Interfaces;

namespace Gico.SystemCommandsHandler
{
    public class CustomerCommandHandler :
        ICommandHandler<CustomerRegisterCommand, ICommandResult>,
        ICommandHandler<CustomerAddCommand, ICommandResult>,
        ICommandHandler<CustomerChangeCommand, ICommandResult>,
        ICommandHandler<CustomerExternalLoginAddCommand, ICommandResult>
    {
        private readonly ICustomerService _customerService;
        private readonly ICommonService _commonService;
        private readonly IEmailSmsService _emailSmsService;
        public CustomerCommandHandler(ICustomerService customerService, ICommonService commonService, IEmailSmsService emailSmsService)
        {
            _customerService = customerService;
            _commonService = commonService;
            _emailSmsService = emailSmsService;
        }

        public async Task<ICommandResult> Handle(CustomerRegisterCommand mesage)
        {
            try
            {
                Customer customer = new Customer(mesage.Version);
                customer.Register(mesage);
                await _customerService.AddToDb(customer);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = customer.Id,
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

        public async Task<ICommandResult> Handle(CustomerAddCommand mesage)
        {
            try
            {
                Customer customer = new Customer(mesage.Version);
                customer.Add(mesage);
                await _customerService.AddToDb(customer);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = customer.Id,
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

        public async Task<ICommandResult> Handle(CustomerChangeCommand mesage)
        {
            try
            {
                ICommandResult result;
                RCustomer customerFromDb = await _customerService.GetFromDb(mesage.Id);
                if (customerFromDb == null)
                {
                    result = new CommandResult()
                    {
                        Message = "",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Customer_NotExist
                    };
                    return result;
                }
                Customer customer = new Customer(customerFromDb);
                string code = string.Empty;
                if (string.IsNullOrEmpty(customerFromDb.Code))
                {
                    long systemIdentity = await _commonService.GetNextId(typeof(Customer));
                    code = Common.Common.GenerateCodeFromId(systemIdentity, 3);
                }
                customer.Change(mesage);
                await _customerService.ChangeToDb(customer, code);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = customer.Id,
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

        public async Task<ICommandResult> Handle(CustomerExternalLoginAddCommand mesage)
        {
            try
            {
                ICommandResult result;
                RCustomer customerFromDb = await _customerService.GetFromDb(mesage.CustomerId);
                if (customerFromDb == null)
                {
                    result = new CommandResult()
                    {
                        Message = "",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.Customer_NotExist
                    };
                    return result;
                }
                customerFromDb.CustomerExternalLogins = await _customerService.GetCustomerExternalLoginByCustomerId(customerFromDb.Id);
                Customer customer = new Customer(customerFromDb);
                CustomerExternalLogin customerExternalLogin = customer.AddExternalLogin(mesage);
                if (customerExternalLogin != null)
                {
                    await _customerService.Change(customer, customerExternalLogin);
                }
                if (!string.IsNullOrEmpty(mesage.VerifyId))
                {
                    RVerify rverify = await _emailSmsService.GetVerifyFromDb(mesage.VerifyId);
                    if (rverify != null)
                    {
                        Verify verify = new Verify(rverify);
                        verify.ChangeStatusToVerified();
                        await _emailSmsService.ChangeVerifyStatus(verify.Id, verify.Status);
                    }
                }
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = customer.Id,
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