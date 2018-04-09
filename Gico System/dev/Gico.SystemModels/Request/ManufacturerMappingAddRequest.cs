﻿using Gico.Config;
using Gico.Models.Request;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Request
{
    public class ManufacturerMappingAddRequest:BaseRequest
    {
        public int ManufacturerId { get; set; }

        public string CategoryId { get; set; }
    }
}
