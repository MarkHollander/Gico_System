using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Mapping
{
    public static class ActionDefineMapping
    {
        public static ActionDefineViewModel ToModel(this RActionDefine actionDefine)
        {
            if (actionDefine == null)
            {
                return null;
            }
            return new ActionDefineViewModel()
            {
                Name = actionDefine.Name,
                Id = actionDefine.Id,
                Group = actionDefine.Group
            };
        }
        public static ActionDefineViewModel ToModel(this RActionDefine actionDefine, bool @checked)
        {
            if (actionDefine == null)
            {
                return null;
            }
            return new ActionDefineViewModel()
            {
                Name = actionDefine.Name,
                Id = actionDefine.Id,
                Group = actionDefine.Group,
                Checked = @checked
            };
        }
    }
}