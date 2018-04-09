using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IMeasureUnitRepository
    {
        Task<RMeasureUnit[]> Search(string unitName, EnumDefine.GiftCodeCampaignStatus unitStatus, RefSqlPaging sqlPaging);
        Task Add(MeasureUnit measure);
        Task Change(MeasureUnit measure);
        Task<RMeasureUnit> GetById(string id);
    }
}
