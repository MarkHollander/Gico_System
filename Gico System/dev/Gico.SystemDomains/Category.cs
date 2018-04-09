using Gico.Domains;
using System;
using Gico.Common;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;

namespace Gico.SystemDomains
{
    public class Category: BaseDomain
    {
        #region Publish method

        public void Add(CategoryAddCommand command)
        {
            Id = Common.Common.GenerateGuid();
            LanguageId = command.LanguageId;
            Code = command.Code;
            CreatedDateUtc = Extensions.GetCurrentDateUtc();
            CreatedUid = command.CreatedUid;
            UpdatedDateUtc = Extensions.GetCurrentDateUtc();
            UpdatedUid = command.CreatedUid??string.Empty;
            ParentId = command.ParentId;
            Name = command.Name;
            Description = command.Description;
            DisplayOrder = command.DisplayOrder;
            Version = command.Version;
            Status = command.Status;
            AddOrChangeEvent();
        }
        public void Change(CategoryChangeCommand command)
        {
            Id = command.Id;
            LanguageId = command.LanguageId;
            Code = command.Code;
            CreatedDateUtc = Extensions.GetCurrentDateUtc();
            CreatedUid = command.CreatedUid;
            UpdatedDateUtc = Extensions.GetCurrentDateUtc();
            UpdatedUid = command.CreatedUid??string.Empty;
            ParentId = command.ParentId;
            Name = command.Name;
            Description = command.Description;
            DisplayOrder = command.DisplayOrder;
            Version = command.Version;
            Status = command.Status;
            AddOrChangeEvent();
        }
        #endregion

        #region Event
        private void AddOrChangeEvent()
        {
            CategoryCacheAddOrChangeEvent @event = ToAddOrChangeEvent();
            AddEvent(@event);
        }
        #endregion

        #region Convert
        public CategoryCacheAddOrChangeEvent ToAddOrChangeEvent()
        {
            return new CategoryCacheAddOrChangeEvent()
            {
                Name = this.Name,
                Id = this.Id,
                LanguageId = this.LanguageId,
                Status = this.Status,
                Description = this.Description,
                ParentId = this.ParentId,
                DisplayOrder = this.DisplayOrder,
                Code = this.Code,
                Version = this.Version
                
           
            };
        }
        #endregion

        #region Properties
        public string Name { get; set; }

        public string ParentId { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public int Version { get; set; }

        #endregion

    }                    
}
