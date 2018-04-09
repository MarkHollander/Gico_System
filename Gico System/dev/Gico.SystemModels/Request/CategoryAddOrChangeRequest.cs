using Gico.Models.Request;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Request
{
    public class CategoryAddOrChangeRequest:BaseRequest
    {
        public CategoryModel Category { get; set; }
    }
}
