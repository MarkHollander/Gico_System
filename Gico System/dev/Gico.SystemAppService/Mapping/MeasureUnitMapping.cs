using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Gico.Common;
using Gico.Models.Response;

namespace Gico.SystemAppService.Mapping
{
    public static class MeasureUnitMapping
    {
        public static MeasureUnitViewModel ToModel(this RMeasureUnit measures)
        {
            if (measures == null)
            {
                return null;
            }
            return new MeasureUnitViewModel()
            {
                UnitId = measures.UnitId,
                UnitName = measures.UnitName,
                UnitStatus = measures.UnitStatus,
                BaseUnitId = measures.BaseUnitId,
                Ratio = measures.Ratio,
                StatusName = measures.UnitStatus.GetDisplayName(),
                UnitNameB = measures.UnitNameB,
            };
        }

        public static KeyValueTypeStringModel ToKeyValueTypeStringModel(this RMeasureUnit measureUnit)
        {
            if (measureUnit == null)
            {
                return null;
            }
            return new KeyValueTypeStringModel()
            {
                Value = measureUnit.UnitId,
                Text = measureUnit.UnitName
            };
        }

        public static MeasureUnitAddCommand ToCommand(this MeasureUnitAddRequest request, string userId)
        {
            if (request == null)
            {
                return null;
            }
            return new MeasureUnitAddCommand()
            {
                UnitName = request.UnitName,
                CreatedOnUtc = Extensions.GetCurrentDateUtc(),
                CreatedUserId = userId,
                BaseUnitId = request.BaseUnitId,
                Ratio = request.Ratio,
                UnitStatus = request.UnitStatus,
            };
        }

        public static MeasureUnitChangeCommand ToCommand(this MeasureUnitChangeRequest request, string userId)
        {
            if (request == null)
            {
                return null;
            }
            return new MeasureUnitChangeCommand()
            {
                UnitId = request.UnitId,
                UnitName = request.UnitName,
                UpdatedOnUtc = Extensions.GetCurrentDateUtc(),
                UpdatedUserId = userId,
                BaseUnitId = request.BaseUnitId,
                Ratio = request.Ratio,
                UnitStatus = request.UnitStatus,
            };
        }
    }
}
