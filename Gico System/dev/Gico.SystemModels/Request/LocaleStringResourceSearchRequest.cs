using Gico.Config;
using Gico.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request
{
    public class LocaleStringResourceSearchRequest
    {
        public KeyValueTypeStringModel[] Languages { get; set; }
        public string LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
