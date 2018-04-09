//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Nop.Core;
//using Nop.Core.Domain.Orders;
//using Nop.Services.Orders;
//using Nop.Web.Factories;
//using Nop.Web.Framework.Components;

//namespace Nop.Web.Components
//{
//    public class OrderTotalsViewComponent : NopViewComponent
//    {
//        private readonly IShoppingCartModelFactory _shoppingCartModelFactory;
        
//        private readonly IWorkContext _workContext;

//        public OrderTotalsViewComponent(IShoppingCartModelFactory shoppingCartModelFactory,
            
//            IWorkContext workContext)
//        {
//            this._shoppingCartModelFactory = shoppingCartModelFactory;
            
//            this._workContext = workContext;
//        }

//        public IViewComponentResult Invoke(bool isEditable)
//        {
//            var cart = _workContext.CurrentCustomer.ShoppingCartItems
//                .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
//                .LimitPerStore(0)
//                .ToList();

//            var model = _shoppingCartModelFactory.PrepareOrderTotalsModel(cart, isEditable);
//            return View(model);
//        }
//    }
//}
