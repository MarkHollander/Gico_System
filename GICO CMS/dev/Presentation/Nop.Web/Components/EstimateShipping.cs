//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Nop.Core;
//using Nop.Core.Domain.Orders;
//using Nop.Services.Orders;
//using Nop.Web.Factories;
//using Nop.Web.Framework.Components;

//namespace Nop.Web.Components
//{
//    public class EstimateShippingViewComponent : NopViewComponent
//    {
//        private readonly IShoppingCartModelFactory _shoppingCartModelFactory;
        
//        private readonly IWorkContext _workContext;

//        public EstimateShippingViewComponent(IShoppingCartModelFactory shoppingCartModelFactory,
            
//            IWorkContext workContext)
//        {
//            this._shoppingCartModelFactory = shoppingCartModelFactory;
            
//            this._workContext = workContext;
//        }

//        public IViewComponentResult Invoke(bool? prepareAndDisplayOrderReviewData)
//        {
//            var cart = _workContext.CurrentCustomer.ShoppingCartItems
//                .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
//                .LimitPerStore(0)
//                .ToList();

//            var model = _shoppingCartModelFactory.PrepareEstimateShippingModel(cart);
//            if (!model.Enabled)
//                return Content("");

//            return View(model);
//        }
//    }
//}
