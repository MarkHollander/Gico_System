﻿using Gico.Config;
using Gico.Models.Request;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Request
{
    public class AttrCategoryChangeRequest : BaseRequest
    {
        public AttrCategoryModel AttrCategory { get; set; }
    }
}
