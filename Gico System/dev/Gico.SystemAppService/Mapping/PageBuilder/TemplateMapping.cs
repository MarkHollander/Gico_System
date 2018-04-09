using Gico.ReadSystemModels.PageBuilder;
using Gico.SystemCommands.PageBuilder;
using Gico.SystemModels.Models.PageBuilder;
using Gico.SystemModels.Request.PageBuilder;
using Gico.Common;

using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;

namespace Gico.SystemAppService.Mapping.PageBuilder
{
    public static class TemplateMapping
    {
        public static TemplateViewModel ToModel(this RTemplate request)
        {
            if (request == null) return null;
            return new TemplateViewModel()
            {
                Id = request.Id,
                Code = request.Code,
                TemplateName = request.TemplateName,
                Thumbnail = string.Format("{0}{1}", ConfigSettingEnum.CdnDomain.GetConfig(), request.Thumbnail),
                PathToView = request.PathToView,
                Status = request.Status,
                Structure = request.Structure,
                PageType = request.PageType,
                PageParameters = request.PageParameters,
                Version = request.Version,
                
            };
        }

        public static TemplateAddCommand ToCommandAdd(this TemplateAddOrChangeRequest request, string userId)
        {
            if (request == null) return null;
            return new TemplateAddCommand
            {
                TemplateName = request.TemplateName,
                Thumbnail = request.Thumbnail.Replace(ConfigSettingEnum.CdnDomain.GetConfig(), string.Empty),
                Structure = request.Structure,
                PathToView = request.PathToView,
                Status = request.Status,
                Code = request.Code,
                PageType = request.PageType,
                PageParameters = request.PageParameters,
                CreatedDateUtc = Extensions.GetCurrentDateUtc(),
                CreatedUid = userId
            };
        }
        public static TemplateChangeCommand ToCommandChange(this TemplateAddOrChangeRequest request, string userId)
        {
            if (request == null) return null;
            return new TemplateChangeCommand
            {
                Id = request.Id,
                TemplateName = request.TemplateName,
                Thumbnail = request.Thumbnail.Replace(ConfigSettingEnum.CdnDomain.GetConfig(), string.Empty),
                Structure = request.Structure,
                PathToView = request.PathToView,
                Status = request.Status,
                Code = request.Code,
                PageType = request.PageType,
                PageParameters = request.PageParameters,
                UpdatedUid = userId
            };
        }

        public static TemplateRemoveCommand ToRemoveCommand(this TemplateRemoveRequest request, string userId)
        {
            if (request == null) return null;
            return new TemplateRemoveCommand()
            {
                Id = request.Id,
                UserId = userId
            };
        }
    }
}
