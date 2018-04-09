using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemService.Implements;
using Gico.SystemService.Interfaces;

namespace Gico.SystemCommandsHandler
{
    public class VendorCommandHandler :  ICommandHandler<VendorAddCommand, ICommandResult>, ICommandHandler<VendorChangeCommand, ICommandResult>
    {
        private readonly IVendorService _vendorService;
        private readonly ICommonService _commonService;
        public VendorCommandHandler(IVendorService vendorService, ICommonService commonService)
        {
            _vendorService = vendorService;
            _commonService = commonService;
        }

        //public async Task<ICommandResult> Handle(VendorRegisterCommand mesage)
        //{
        //    try
        //    {
        //        Vendor vendor = new Vendor(mesage.Version);
        //        vendor.Register(mesage);
        //        await _vendorService.AddToDb(vendor);
        //        ICommandResult result = new CommandResult()
        //        {
        //            Message = "",
        //            ObjectId = vendor.Id,
        //            Status = CommandResult.StatusEnum.Sucess
        //        };
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        e.Data["Param"] = mesage;
        //        ICommandResult result = new CommandResult()
        //        {
        //            Message = e.Message,
        //            Status = CommandResult.StatusEnum.Fail
        //        };
        //        return result;
        //    }
        //}

        public async Task<ICommandResult> Handle(VendorAddCommand mesage)
        {
            try
            {
                Vendor vendor = new Vendor(mesage.Version);
                vendor.Add(mesage);
                await _vendorService.AddToDb(vendor);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = vendor.Id,
                    Status = CommandResult.StatusEnum.Sucess
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

        public async Task<ICommandResult> Handle(VendorChangeCommand mesage)
        {
            try
            {
                Vendor vendor = new Vendor(mesage.Version);
                RVendor vendorFromDb = await _vendorService.GetFromDb(mesage.Id);
                string code = string.Empty;
                if (string.IsNullOrEmpty(vendorFromDb.Code))
                {
                    long systemIdentity = await _commonService.GetNextId(typeof(Vendor));
                    code = Common.Common.GenerateCodeFromId(systemIdentity, 3);
                }
                vendor.Change(mesage);
                await _vendorService.ChangeToDb(vendor, code);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = vendor.Id,
                    Status = CommandResult.StatusEnum.Sucess
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