using System.Collections.Generic;
using Gico.FrontEndModels.Request;
using Microsoft.AspNetCore.Http;

namespace Gico.FrontEndModels.Models
{
    public class PageModel
    {
        public PageModel()
        {
            Resources = new Dictionary<string, string>();
            Messages = new List<string>();
            IsSuccess = true;
        }

        public PageModel(PageModel model) : this()
        {
            SetInitInfo(model);
        }

        public void SetInitInfo(PageModel model)
        {
            LanguageId = model.LanguageId;
            Seo = model.Seo;
            MenuHeaders = model.MenuHeaders;
            MenuFooters = model.MenuFooters;
            Resources = model.Resources;
            Messages = model.Messages;
            IsSuccess = model.IsSuccess;
        }

        public string LanguageId { get; set; }
        public SeoModel Seo { get; set; }
        public IList<MenuModel> MenuHeaders { get; set; }
        public IList<MenuModel> MenuFooters { get; set; }
        public IDictionary<string, string> Resources { get; set; }
        public string T(string key)
        {
            return Resources.ContainsKey(key) ? Resources[key] : key;
        }

        public IList<string> Messages { get; set; }
        public bool IsSuccess { get; set; }
        public int HttpResponseCode { get; set; }
        public void AddMessage(string key)
        {
            string message = T(key);
            Messages.Add(message);
            IsSuccess = false;
        }
    }

    public class AjaxModel
    {
        public AjaxModel()
        {
            Resources = new Dictionary<string, string>();
            Messages = new List<string>();
            IsSuccess = true;
        }
        public AjaxModel(AjaxModel model) : this()
        {
            SetInitInfo(model);
        }

        public void SetInitInfo(AjaxModel model)
        {
            LanguageId = model.LanguageId;
            Resources = model.Resources;
            Messages = model.Messages;
            IsSuccess = model.IsSuccess;
           // IsValid = model.IsValid;
        }
        public string LanguageId { get; set; }
        public IDictionary<string, string> Resources { get; set; }
        public string T(string key)
        {
            return Resources.ContainsKey(key) ? Resources[key] : key;
        }
        public IList<string> Messages { get; set; }
        public bool IsSuccess { get; set; }
        //public bool IsValid { get; set; }
        public void AddMessage(string key)
        {
            string message = T(key);
            Messages.Add(message);
            IsSuccess = false;
        }
    }
}
