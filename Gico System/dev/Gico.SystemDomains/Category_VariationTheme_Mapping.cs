using Gico.Domains;
using System;
using Gico.Common;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;

namespace Gico.SystemDomains
{
    public class Category_VariationTheme_Mapping: BaseDomain
    {


        #region Publish method

        public void Add(Category_VariationTheme_Mapping_AddCommand command)
        {
            VariationThemeId = command.VariationThemeId;
            CategoryId = command.CategoryId;
           
            AddEvent();

        }
      

        public void Remove(Category_VariationTheme_Mapping_RemoveCommand command)
        {
            VariationTheme_Id = command.VariationThemeId;
            CategoryId = command.CategoryId;
            RemoveEvent();
        }

        #endregion


        #region Event
        private void AddEvent()
        {
            Category_VariationTheme_MappingAddEvent @event = ToAddEvent();
            AddEvent(@event);
        }

        private void RemoveEvent()
        {
            Category_VariationTheme_MappingRemoveEvent @event = ToRemoveEvent();
            AddEvent(@event);
        }

        #endregion


        #region Convert

        public Category_VariationTheme_MappingAddEvent ToAddEvent()
        {
            return new Category_VariationTheme_MappingAddEvent()
            {   
                VariationThemeId = this.VariationThemeId,
                CategoryId = this.CategoryId
            };
        }

        public Category_VariationTheme_MappingRemoveEvent ToRemoveEvent()
        {
            return new Category_VariationTheme_MappingRemoveEvent()
            {
                VariationThemeId = this.VariationTheme_Id,
                CategoryId = this.CategoryId

            };
        }
        #endregion




        #region Properties
        public int[] VariationThemeId { get; set; }

        public string CategoryId { get; set; }

        public int VariationTheme_Id { get; set; }
        #endregion






    }
}
