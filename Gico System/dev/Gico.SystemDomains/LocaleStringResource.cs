using Gico.Domains;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemDomains
{
    public class LocaleStringResource : BaseDomain
    {
        public LocaleStringResource()
        {

        }
        public LocaleStringResource(RLocaleStringResource localeStringResource)
        {
            ResourceName = localeStringResource.ResourceName;
            ResourceValue = localeStringResource.ResourceValue;
        }
        public string ResourceName { get; private set; }
        public string ResourceValue { get; private set; }

        public void Add(LocaleStringResourceAddCommand message)
        {
            Id = message.Id;
            LanguageId = message.LanguageId;
            ResourceName = message.ResourceName;
            ResourceValue = message.ResourceValue;
        }

        public void Change(LocaleStringResourceChangeCommand message)
        {
            Id = message.Id;
            LanguageId = message.LanguageId;
            ResourceName = message.ResourceName;
            ResourceValue = message.ResourceValue;
        }

        #region Event
        private void AddOrChangeEvent()
        {
            LocaleStringResourceCacheAddOrChangeEvent @event = ToAddOrChangeEvent();
            AddEvent(@event);
        }
        #endregion


        #region Convert

        public LocaleStringResourceCacheAddOrChangeEvent ToAddOrChangeEvent()
        {
            return new LocaleStringResourceCacheAddOrChangeEvent()
            {
                Id = this.Id,
                LanguageId = this.LanguageId,
                ResourceName = this.ResourceName,
                ResourceValue = this.ResourceValue
            };
        }

        #endregion
    }
}
