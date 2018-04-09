using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<RManufacturer[]> GetAll();
        Task<RManufacturer[]> Search(string name, EnumDefine.StatusEnum status, RefSqlPaging paging);

        Task<RManufacturer> GetById(string id);
        Task<RManufacturer[]> GetById(string[] ids);
        Task Add(Manufacturer manufacturer);

        Task<bool> Change(Manufacturer manufacturer);

    }
}
