using System;
using Gico.Common;
using Gico.Domains;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;

namespace Gico.SystemDomains
{
    public class Language : BaseDomain
    {

        #region Publish method
        public void Init(RLanguage command)
        {
            Id = command.Id;
            CreatedDateUtc = Extensions.GetCurrentDateUtc();
            CreatedUid = string.Empty;
            UpdatedDateUtc = DateTime.UtcNow;
            UpdatedUid = string.Empty;
            Name = command.Name;
            Culture = command.Culture;
            UniqueSeoCode = command.UniqueSeoCode;
            FlagImageFileName = command.FlagImageFileName;
            Rtl = command.Rtl;
            LimitedToStores = command.LimitedToStores;
            DefaultCurrencyId = command.DefaultCurrencyId;
            Published = command.Published;
            DisplayOrder = command.DisplayOrder;
        }
        #endregion
        #region Event
        private void AddOrChangeEvent()
        {
            LanguageCacheAddOrChangeEvent @event = ToAddOrChangeEvent();
            AddEvent(@event);
        }

        #endregion

        #region Properties
        public string Name { get; private set; }
        public string Culture { get; private set; }
        public string UniqueSeoCode { get; private set; }
        public string FlagImageFileName { get; private set; }
        public bool Rtl { get; private set; }
        public bool LimitedToStores { get; private set; }
        public int DefaultCurrencyId { get; private set; }
        public bool Published { get; private set; }
        public int DisplayOrder { get; private set; }
        #endregion

        #region Convert

        public LanguageCacheAddOrChangeEvent ToAddOrChangeEvent()
        {
            return new LanguageCacheAddOrChangeEvent()
            {
                Name = this.Name,
                Id = this.Id,
                DefaultCurrencyId = this.DefaultCurrencyId,
                LimitedToStores = this.LimitedToStores,
                DisplayOrder = this.DisplayOrder,
                UniqueSeoCode = this.UniqueSeoCode,
                Published = this.Published,
                FlagImageFileName = this.FlagImageFileName,
                Rtl = this.Rtl,
                Culture = this.Culture,
                
            };
        }

        #endregion

        #region publish method
        public void Add(LanguageAddCommand command)
        { 
            Name = command.Name ?? string.Empty;
            Culture = command.Culture ?? string.Empty;
            UniqueSeoCode = command.UniqueSeoCode ?? string.Empty;
            FlagImageFileName = command.FlagImageFileName ?? string.Empty;
            Published = command.Published ;
            DisplayOrder = command.DisplayOrder ;
        }

        public void Change(LanguageChangeCommand command)
        {
            Name = command.Name ?? string.Empty;
            Culture = command.Culture ?? string.Empty;
            UniqueSeoCode = command.UniqueSeoCode ?? string.Empty;
            FlagImageFileName = command.FlagImageFileName ?? string.Empty;
            Published = command.Published;
            DisplayOrder = command.DisplayOrder;
            Id = command.Id.ToString() ?? string.Empty;   
        }

        #endregion
    }
}