namespace Gico.FrontEndModels.Models
{
    public class ProductDetailViewModel : PageModel
    {
        public ProductDetailViewModel() 
        {
        }

        public ProductDetailViewModel(PageModel model) : base(model)
        {
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}