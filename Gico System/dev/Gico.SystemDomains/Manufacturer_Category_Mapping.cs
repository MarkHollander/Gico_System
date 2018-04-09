using Gico.Domains;
using System;
using Gico.Common;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;
using Gico.Config;

namespace Gico.SystemDomains
{
    public class Manufacturer_Category_Mapping : BaseDomain
    {

        #region Publish method
        public void Remove(ManufacturerCategoryMappingRemoveCommand command)
        {
            ManufacturerId = command.ManufacturerId;
            CategoryId = command.CategoryId;
            RemoveEvent();
        }


        public void Add(ManufacturerCategoryMappingAddCommand command)
        {
            ManufacturerId = command.ManufacturerId;
            CategoryId = command.CategoryId;
            AddOrChangeEvent();

        }


        #endregion

        #region Event


        private void RemoveEvent()
        {
            ManufacturerCategoryCacheRemoveEvent @event = ToRemoveEvent();
            AddEvent(@event);
        }

        private void AddOrChangeEvent()
        {
            ManufacturerCategoryMappingCacheAddEvent @event = ToAddOrChangeEvent();
            AddEvent(@event);
        }


        #endregion


        #region Convert

        public ManufacturerCategoryCacheRemoveEvent ToRemoveEvent()
        {
            return new ManufacturerCategoryCacheRemoveEvent()
            {
                ManufacturerId = this.ManufacturerId,
                CategoryId = this.CategoryId

            };
        }

        public ManufacturerCategoryMappingCacheAddEvent ToAddOrChangeEvent()
        {
            return new ManufacturerCategoryMappingCacheAddEvent()
            {
                CategoryId = CategoryId,
                ManufacturerId = ManufacturerId

            };
        }


        #endregion


        #region Property
        public int ManufacturerId { get; set; }
        public string CategoryId { get; set; }
        #endregion
    }
}
