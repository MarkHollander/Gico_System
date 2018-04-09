using System;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.Domains;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;
using static Gico.Config.EnumDefine;

namespace Gico.SystemDomains
{
    public class Vendor : BaseDomain, IVersioned
    {
        public Vendor(int version)
        {
            Version = version;
        }

        #region Publish method
        //public void Register(VendorRegisterCommand command)
        //{
        //    Id = Common.Common.GenerateGuid();
        //    Code = Common.Common.GenerateCodeFromId(command.SystemNumericalOrder, 3);
        //    string passwordHash = EncryptionExtensions.Encryption(Code, command.Password, out string salt);
        //    CreatedDateUtc = command.CreatedDateUtc;
        //    CreatedUid = string.Empty;
        //    UpdatedDateUtc = command.CreatedDateUtc;
        //    UpdatedUid = string.Empty;
        //    Email = command.Email;
        //    PhoneNumber = command.Mobile;
        //    EmailConfirmed = false;
        //    IsTaxExempt = false;
        //    FailedLoginAttempts = 0;
        //    LastIpAddress = command.RegisterIp;
        //    Password = passwordHash;
        //    PasswordSalt = salt;
        //    PhoneNumberConfirmed = false;
        //    TwoFactorEnabled = EnumDefine.TwoFactorEnum.Disable;
        //    FullName = command.FullName;
        //    Gender = command.Gender;
        //    Birthday = command.Birthday;
        //    AddType(TypeEnum.IsVendor);
        //    Status = StatusEnum.New;
        //    RegisterEvent();
        //}

        public void Add(VendorAddCommand command)
        {
            Id = Common.Common.GenerateGuid();
            Code = command.Code;
           
            Email = command.Email ?? string.Empty;
            Name = command.Name ?? string.Empty;
            CompanyName = command.CompanyName ?? string.Empty; ;
            Description = command.Description ?? string.Empty;
            Logo = command.Logo ?? string.Empty; ;
            Phone = command.Phone ?? string.Empty;
            Fax = command.Fax ?? string.Empty; ;
            Description = command.Description ?? string.Empty;
            Website = command.Website;
            Type = command.Type;
            Status = command.Status;
            CreatedDateUtc = command.CreatedDateUtc;
            UpdatedDateUtc = command.CreatedDateUtc;
            CreatedUid = command.CreatedUid ?? string.Empty;
            UpdatedUid = command.CreatedUid ?? string.Empty;
            Version = command.Version;
        }
        public void Change(VendorChangeCommand command)
        {
            Add(command);
            Id = command.Id ?? string.Empty;
            UpdatedDateUtc = command.UpdatedDateUtc;
            UpdatedUid = command.UpdatedUid ?? string.Empty;
        }
        public void Active()
        {
            Status = StatusEnum.Active;
        }
        public void Lock()
        {
            Status = StatusEnum.Lock;
        }
        public void AddType(TypeEnum type)
        {
            Type |= type;
        }
        public void RemoveType(TypeEnum type)
        {
            Type &= ~type;
        }
        public bool CheckType(TypeEnum type)
        {
            return Type.HasFlag(type);
        }
      

        #endregion

        #region Event
        private void RegisterEvent()
        {
           // VendorCacheRegisterEvent @event = new VendorCacheRegisterEvent();
            //@event.Registered(Id, Email, PasswordHash, CreatedDateUtc);
         //   AddEvent(@event);
        }
        #endregion

        #region Properties
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string CompanyName { get; private set; }
        public string Description { get; private set; }
        public string Logo { get; private set; }
        public string Phone { get; private set; }
        public string Fax { get; private set; }
        public string Website { get; private set; }
        public EnumDefine.TypeEnum Type { get; private set; }
        public new EnumDefine.StatusEnum Status { get; private set; }
        public int Version { get; private set; }
        #endregion


    }
}