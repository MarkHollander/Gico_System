using Gico.ReadSystemModels.PageBuilder;
using Gico.SystemCommands.PageBuilder;
using Gico.SystemModels.Models.PageBuilder;
using Gico.SystemModels.Request.PageBuilder;
using Gico.Common;

using System;
using System.Collections.Generic;
using System.Text;
using Gico.Models.Response;

namespace Gico.SystemAppService.Mapping.PageBuilder
{
    public static class TemplateConfigMapping
    {
        public static TemplateConfigViewModel ToModel(this RTemplateConfig request)
        {
            if (request == null) return new TemplateConfigViewModel();
            return new TemplateConfigViewModel()
            {
                Id = request.Id,
                TemplateId = request.TemplateId,
                TemplatePositionCode = request.TemplatePositionCode,
                ComponentId = request.ComponentId,
                PathToView = request.PathToView,
                Status = request.Status,
                ComponentType = request.ComponentType,
                DataSource = request.DataSource,
                Version = request.Version,
            };
        }
        public static TemplateConfigViewModel ToModel(this RTemplateConfig request, KeyValueTypeStringModel component)
        {
            if (request == null) return new TemplateConfigViewModel();
            var model = request.ToModel();
            model.Component = component;
            return model;
        }
        public static TemplateConfigAddCommand ToCommandAdd(this TemplateConfigAddOrChangeRequest request, string userId)
        {
            if (request == null) return null;
            return new TemplateConfigAddCommand
            {
                TemplateId = request.TemplateId,
                TemplatePositionCode = request.TemplatePositionCode,
                ComponentId = request.ComponentId,
                PathToView = request.PathToView,
                Status = request.Status,
                ComponentType = request.ComponentType,
                DataSource = request.DataSource,
                CreatedDateUtc = Extensions.GetCurrentDateUtc(),
                CreatedUid = userId
            };
        }
        public static TemplateConfigChangeCommand ToCommandChange(this TemplateConfigAddOrChangeRequest request, string userId)
        {
            if (request == null) return null;
            return new TemplateConfigChangeCommand
            {
                Id = request.Id,
                TemplateId = request.TemplateId,
                TemplatePositionCode = request.TemplatePositionCode,
                ComponentId = request.ComponentId,
                PathToView = request.PathToView,
                Status = request.Status,
                ComponentType = request.ComponentType,
                DataSource = request.DataSource,
                UpdatedUid = userId
            };
        }

        public static TemplateConfigRemoveCommand ToCommandRemove(this TemplateConfigRemoveRequest request,
            string userId)
        {
            if (request == null)
            {
                return null;
            }
            return new TemplateConfigRemoveCommand()
            {
                Id = request.Id,
                UserId = userId,
                TemplateId = request.TemplateId
            };
        }
    }
}
