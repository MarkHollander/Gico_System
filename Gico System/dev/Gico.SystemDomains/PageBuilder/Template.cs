using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.Domains;
using Gico.SystemCommands.PageBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gico.ReadSystemModels.PageBuilder;
using Gico.SystemEvents.Cache;
using Gico.SystemEvents.Cache.PageBuilder;
using static Gico.Config.EnumDefine;

namespace Gico.SystemDomains.PageBuilder
{
    public class Template : BaseDomain
    {
        public Template()
        {
        }

        public Template(RTemplate rTemplate)
        {
            Id = rTemplate.Id;
            TemplateName = rTemplate.TemplateName;
            Thumbnail = rTemplate.Thumbnail;
            Structure = rTemplate.Structure;
            PathToView = rTemplate.PathToView;
            Status = rTemplate.Status;
            Code = rTemplate.Code;
            PageType = rTemplate.PageType;
            PageParameters = rTemplate.PageParameters;
            UpdatedDateUtc = rTemplate.UpdatedDateUtc;
            CreatedDateUtc = rTemplate.CreatedDateUtc;
            CreatedUid = rTemplate.CreatedUid;
            UpdatedUid = rTemplate.UpdatedUid;
        }
        public Template(RTemplate rTemplate, IList<RTemplateConfig> rTemplateConfigs) : this(rTemplate)
        {
            TemplateConfigs = rTemplateConfigs?.Select(p => new TemplateConfig(p)).ToList();
        }

        #region Publish method        
        public void Add(TemplateAddCommand command, bool addEvent = true)
        {
            Id = Common.Common.GenerateGuid();
            Code = command.Code;
            TemplateName = command.TemplateName ?? string.Empty;
            Thumbnail = command.Thumbnail ?? string.Empty;
            Structure = command.Structure ?? string.Empty;
            PathToView = command.PathToView ?? string.Empty;
            Status = command.Status;
            PageType = command.PageType;
            PageParameters = command.PageParameters ?? string.Empty;
            CreatedDateUtc = command.CreatedDateUtc;
            UpdatedDateUtc = command.CreatedDateUtc;
            CreatedUid = command.CreatedUid ?? string.Empty;
            UpdatedUid = command.CreatedUid ?? string.Empty;
            if (addEvent)
                AddEvent(ToAddOrChangeCacheEvent());
        }
        public void Change(TemplateChangeCommand command)
        {
            Add(command, false);
            Id = command.Id;
            UpdatedDateUtc = command.UpdatedDateUtc;
            UpdatedUid = command.UpdatedUid ?? string.Empty;

            AddEvent(ToAddOrChangeCacheEvent());
        }
        public void ChangeStatus(EnumDefine.CommonStatusEnum status, DateTime updatedDate, string uid)
        {
            Status = status;
            UpdatedDateUtc = updatedDate;
            UpdatedUid = uid;

            if (status == CommonStatusEnum.Deleted)
            {
                AddEvent(ToRemoveCacheEvent());
            }
            else
            {
                AddEvent(ToAddOrChangeCacheEvent());
            }

        }
        public TemplateConfig AddTemplateconfig(TemplateConfigAddCommand command)
        {
            if (TemplateConfigs == null)
            {
                TemplateConfigs = new List<TemplateConfig>();
            }
            TemplateConfig templateConfig = new TemplateConfig();
            templateConfig.Init(command);
            TemplateConfigs.Add(templateConfig);

            AddEvent(ToAddOrChangeCacheEvent());
            return templateConfig;
        }
        public TemplateConfig ChangeTemplateconfig(TemplateConfigChangeCommand command)
        {
            if (TemplateConfigs == null)
            {
                TemplateConfigs = new List<TemplateConfig>();
            }
            TemplateConfig templateConfig = TemplateConfigs.FirstOrDefault(p => p.Id == command.Id);
            if (templateConfig == null)
            {
                throw new Exception("TemplateConfig not found");
            }
            templateConfig.Change(command);

            AddEvent(ToAddOrChangeCacheEvent());
            return templateConfig;
        }
        public TemplateConfig RemoveTemplateconfig(TemplateConfigRemoveCommand command)
        {
            if (TemplateConfigs == null)
            {
                TemplateConfigs = new List<TemplateConfig>();
            }
            TemplateConfig templateConfig = TemplateConfigs.FirstOrDefault(p => p.Id == command.Id);
            if (templateConfig == null)
            {
                throw new Exception("TemplateConfig not found");
            }
            templateConfig.Remove(command);

            AddEvent(ToAddOrChangeCacheEvent());
            return templateConfig;
        }
        #endregion

        #region Properties
        public string TemplateName { get; private set; }
        public string Thumbnail { get; private set; }
        public string Structure { get; private set; }
        public string PathToView { get; private set; }
        public new EnumDefine.CommonStatusEnum Status { get; private set; }
        public EnumDefine.TemplatePageTypeEnum PageType { get; private set; }
        public string PageParameters { get; set; }
        public List<TemplateConfig> TemplateConfigs { get; private set; }
        #endregion
        #region Convert

        public TemplateCacheEvent ToAddOrChangeCacheEvent()
        {
            return new TemplateCacheEvent()
            {
                Id = this.Id,
                TemplateName = this.TemplateName,
                Thumbnail = this.Thumbnail,
                Structure = this.Structure,
                PathToView = this.PathToView,
                Status = this.Status,
                Code = this.Code,
                PageType = this.PageType,
                PageParameters = this.PageParameters,
                UpdatedDateUtc = this.UpdatedDateUtc,
                CreatedDateUtc = this.CreatedDateUtc,
                CreatedUid = this.CreatedUid,
                UpdatedUid = this.UpdatedUid,
                TemplateConfigs = this.TemplateConfigs?.Select(p => p.ToEvent()).ToArray()
            };
        }

        public TemplateCacheRemoveEvent ToRemoveCacheEvent()
        {
            return new TemplateCacheRemoveEvent()
            {
                Id = this.Id
            };
        }

        #endregion
    }
}
