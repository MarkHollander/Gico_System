using System;
using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemEvents.Cache;
using Gico.SystemService.Interfaces;

namespace Gico.SystemEventsHandler
{
    public class RoleEventHandler : IEventHandler<ActionDefineAddEvent>
    {
        private readonly IRoleService _roleService;

        public RoleEventHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task Handle(ActionDefineAddEvent mesage)
        {
            try
            {
                RActionDefine actionDefine = new RActionDefine()
                {
                    Id = mesage.Id,
                    Name = mesage.Name
                };
                await _roleService.AddToCache(actionDefine);
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                throw e;
            }
        }
    }
}