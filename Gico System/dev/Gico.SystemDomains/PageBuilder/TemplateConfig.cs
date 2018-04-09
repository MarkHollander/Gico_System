using Gico.Config;
using Gico.Domains;
using Gico.SystemCommands.PageBuilder;
using System;
using System.Collections.Generic;
using System.Text;
using Gico.ReadSystemModels.PageBuilder;
using Gico.SystemEvents.Cache.PageBuilder;

namespace Gico.SystemDomains.PageBuilder
{
    public class TemplateConfig : BaseDomain
    {
        public TemplateConfig()
        {
        }

        public TemplateConfig(RTemplateConfig rTemplateConfig)
        {
            Id = rTemplateConfig.Id;
            TemplateId = rTemplateConfig.TemplateId;
            TemplatePositionCode = rTemplateConfig.TemplatePositionCode;
            ComponentType = rTemplateConfig.ComponentType;
            ComponentId = rTemplateConfig.ComponentId;
            PathToView = rTemplateConfig.PathToView;
            Status = rTemplateConfig.Status;
            DataSource = rTemplateConfig.DataSource ?? string.Empty;
            UpdatedDateUtc = rTemplateConfig.UpdatedDateUtc;
            CreatedDateUtc = rTemplateConfig.CreatedDateUtc;
            CreatedUid = rTemplateConfig.CreatedUid;
            UpdatedUid = rTemplateConfig.UpdatedUid;
        }

        #region Publish method        
        public void Init(TemplateConfigAddCommand command)
        {
            Id = Common.Common.GenerateGuid();
            TemplateId = command.TemplateId ?? string.Empty;
            TemplatePositionCode = command.TemplatePositionCode ?? string.Empty;
            ComponentId = command.ComponentId ?? string.Empty;
            PathToView = command.PathToView ?? string.Empty;
            Status = command.Status;
            ComponentType = command.ComponentType;
            DataSource = command.DataSource ?? string.Empty;
            CreatedDateUtc = command.CreatedDateUtc;
            UpdatedDateUtc = command.CreatedDateUtc;
            CreatedUid = command.CreatedUid ?? string.Empty;
            UpdatedUid = command.CreatedUid ?? string.Empty;
        }
        public void Change(TemplateConfigChangeCommand command)
        {
            Init(command);
            Id = command.Id;
            UpdatedDateUtc = command.UpdatedDateUtc;
            UpdatedUid = command.UpdatedUid ?? string.Empty;
        }

        public void Remove(TemplateConfigRemoveCommand command)
        {
            Status = EnumDefine.CommonStatusEnum.Deleted;
            UpdatedUid = command.UserId;
            UpdatedDateUtc = command.CreatedDateUtc;
        }
        #endregion

        #region Properties
        public string TemplateId { get; private set; }
        public string TemplatePositionCode { get; private set; }
        public EnumDefine.TemplateConfigComponentTypeEnum ComponentType { get; private set; }
        public string ComponentId { get; private set; }
        public string PathToView { get; private set; }
        public new EnumDefine.CommonStatusEnum Status { get; private set; }
        public string DataSource { get; private set; }

        #endregion
        #region Convert

        public TemplateConfigCacheEvent ToEvent()
        {
            return new TemplateConfigCacheEvent()
            {
                Id = this.Id,
                TemplateId = this.TemplateId,
                TemplatePositionCode = this.TemplatePositionCode,
                ComponentType = this.ComponentType,
                ComponentId = this.ComponentId,
                PathToView = this.PathToView,
                Status = this.Status,
                DataSource = this.DataSource,
                UpdatedDateUtc = this.UpdatedDateUtc,
                CreatedDateUtc = this.CreatedDateUtc,
                CreatedUid = this.CreatedUid,
                UpdatedUid = this.UpdatedUid,

            };
        }

        #endregion
    }
}
