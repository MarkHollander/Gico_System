using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;
namespace Gico.SystemModels.Response
{
    public class CategoryGetResponse : BaseResponse
    {
        public CategoryGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CategoryStatus), false);
        }
        public CategoryModel[] Parents { get; set; }

        public KeyValueTypeStringModel[] Languages { get; set; }
        public CategoryModel Category { get; set; }

        public KeyValueTypeIntModel[] Statuses { get; private set; }

    }
}
