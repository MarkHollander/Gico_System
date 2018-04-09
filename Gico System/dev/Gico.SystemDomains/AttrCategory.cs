using Gico.Domains;
using System;
using Gico.Common;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;
using Gico.Config;

namespace Gico.SystemDomains
{
    public class AttrCategory : BaseDomain
    {

        #region Publish method

        public void Add(AttrCategoryAddCommand command)
        {
            AttributeId = command.AttributeId;
            CategoryId = command.CategoryId;
            IsFilter = command.IsFilter;
            FilterSpan = command.FilterSpan;
            BaseUnitId = command.BaseUnitId;
            AttributeType = command.AttributeType;
            DisplayOrder = command.DisplayOrder;
            IsRequired = command.IsRequired;
            AddOrChangeEvent();

        }
        public void Change(AttrCategoryChangeCommand command)
        {
            AttributeId = command.AttributeId;
            CategoryId = command.CategoryId;
            IsFilter = command.IsFilter;
            FilterSpan = command.FilterSpan;
            BaseUnitId = command.BaseUnitId;
            AttributeType = command.AttributeType;
            DisplayOrder = command.DisplayOrder;
            IsRequired = command.IsRequired;
            AddOrChangeEvent();
        }

        public void Remove(AttrCategoryRemoveCommand command)
        {
            AttributeId = command.AttributeId;
            CategoryId = command.CategoryId;    
            RemoveEvent();
        }

        #endregion


        #region Event
        private void AddOrChangeEvent()
        {
            AttrCategoryCachedAddOrChangeEvent @event = ToAddOrChangeEvent();
            AddEvent(@event);
        }

        private void RemoveEvent()
        {
            AttrCategoryCachedRemoveEvent @event = ToRemoveEvent();
            AddEvent(@event);
        }

        #endregion


        #region Convert

        public AttrCategoryCachedAddOrChangeEvent ToAddOrChangeEvent()
        {
            return new AttrCategoryCachedAddOrChangeEvent()
            {
                AttributeId = this.AttributeId,
                AttributeType = this.AttributeType,
                BaseUnitId = this.BaseUnitId,
                CategoryId = this.CategoryId,
                DisplayOrder = this.DisplayOrder,
                FilterSpan = this.FilterSpan,
                IsFilter = this.IsFilter,
                IsRequired = this.IsRequired

            };
        }

        public AttrCategoryCachedRemoveEvent ToRemoveEvent()
        {
            return new AttrCategoryCachedRemoveEvent()
            {
                AttributeId = this.AttributeId,
                CategoryId = this.CategoryId

            };
        }
        #endregion



        #region Properties
        public int AttributeId { get; set; }

        public string CategoryId { get; set; }

        public bool IsFilter { get; set; }
        public string FilterSpan { get; set; }
        public int BaseUnitId { get; set; }

        public EnumDefine.AttrCategoryType AttributeType { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRequired { get; set; }
        #endregion
    }
}
