using System;
using Gico.Config;
using Gico.Models.Models;

namespace Gico.SystemModels.Models
{
    public class LanguageViewModel : BaseViewModel
    {
        #region Properties
        public string Name { get;  set; }
        public string Culture { get;  set; }
        public string UniqueSeoCode { get;  set; }
        public string FlagImageFileName { get;  set; }
        public bool Published { get;  set; }
        public int DisplayOrder { get;  set; }
        #endregion

    }
}