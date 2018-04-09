using System.Collections.Generic;

namespace Gico.FrontEndModels.Models
{
    public class CartViewModel : AjaxModel
    {
        public CartViewModel()
        {
            CartItems = new CartItemViewModel[0];
        }

        public CartViewModel(AjaxModel model) : base(model)
        {
        }

        public CartItemViewModel[] CartItems { get; set; }



    }
}