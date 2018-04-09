using System;
using Gico.Config;
using Gico.Models.Models;

namespace Gico.SystemModels.Models
{
    public class ManufacturerCategoryMappingModel:BaseModel
    {
        public int ManufacturerId { get; set; }
        public string CategoryId { get; set; }
    }
}
