using Gico.FrontEndModels.Models;
using Gico.ReadCartModels;

namespace Gico.FrontEndAppService.Mapping
{
    public static class CartMapping
    {
        public static CartViewModel ToModel(this RCart model,string languageId)
        {
            if (model == null) return null;
            return new CartViewModel()
            {


            };
        }

    }
}