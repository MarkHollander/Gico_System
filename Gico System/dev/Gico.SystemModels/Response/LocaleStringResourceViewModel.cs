using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Response
{
    public class LocaleStringResourceViewModel
    {
        public string Id { get; set; }
        public string LanguageName { get; set; }
        public string LanguageId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public bool Status { get; set; }
    }
}
