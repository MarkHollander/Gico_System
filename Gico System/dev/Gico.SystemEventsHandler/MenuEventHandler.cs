using System;
using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemEvents.Cache;

namespace Gico.SystemEventsHandler
{
    public class MenuEventHandler : IEventHandler<MenuCacheAddOrChangeEvent>
    {
        private readonly IMenuCacheStorage _menuCacheStorage;

        public MenuEventHandler(IMenuCacheStorage menuCacheStorage)
        {
            _menuCacheStorage = menuCacheStorage;
        }

        public async Task Handle(MenuCacheAddOrChangeEvent mesage)
        {
            try
            {
                RMenu menu = new RMenu()
                {
                    Id = mesage.Id,
                    Name = mesage.Name,
                    Type = mesage.Type,
                    LanguageId = mesage.LanguageId,
                    StoreId = mesage.StoreId,
                    Url = mesage.Url,
                    Condition = mesage.Condition,
                    Status = mesage.Status,
                    Position = mesage.Position,
                    ParentId = mesage.ParentId,
                    Priority = mesage.Priority
                };
                await _menuCacheStorage.AddOrChange(menu);
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                throw e;
            }
        }
    }
}
