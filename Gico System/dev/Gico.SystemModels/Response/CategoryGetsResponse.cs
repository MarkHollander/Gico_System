using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class CategoryGetsResponse : BaseResponse
    {
        public CategoryGetsResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CategoryStatus), false);
        }

        public KeyValueTypeStringModel[] Languages { get; set; }

        public CategoryModel[] Categories { get; set; }


        public string LanguageDefaultId { get; set; }

        public KeyValueTypeIntModel[] Statuses { get; private set; }



    }
}
