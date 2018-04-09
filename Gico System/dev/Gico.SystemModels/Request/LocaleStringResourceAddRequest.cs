using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request
{
    public class LocaleStringResourceAddRequest : BaseRequest
    {
        public string LanguageId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
    }
}
