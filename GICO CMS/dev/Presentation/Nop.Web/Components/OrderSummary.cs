//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Nop.Core;
//using Nop.Core.Domain.Orders;
//using Nop.Services.Orders;
//using Nop.Web.Factories;
//using Nop.Web.Framework.Components;
//using Nop.Web.Models.ShoppingCart;

//namespace Nop.Web.Components
//{
//    public class OrderSummaryViewComponent : NopViewComponent
//    {
//        private readonly IShoppingCartModelFactory _shoppingCartModelFactory;
        
//        private readonly IWorkContext _workContext;

//        public OrderSummaryViewComponent(IShoppingCartModelFactory shoppingCartModelFactory,
            
//            IWorkContext workContext)
//        {
//            this._shoppingCartModelFactory = shoppingCartModelFactory;
            
//            this._workContext = workContext;
//        }

//        public IViewComponentResult Invoke(bool? prepareAndDisplayOrderReviewData, ShoppingCartModel overriddenModel)
//        {
//            //use already prepared (shared) model
//            if (overriddenModel != null)
//                return View(overriddenModel);

//            //if not passed, then create a new model
//            var cart = _workContext.CurrentCustomer.ShoppingCartItems
//                .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
//                .LimitPerStore(0)
//                .ToList();

//            var model = new ShoppingCartModel();
//            model = _shoppingCartModelFactory.PrepareShoppingCartModel(model, cart,
//                isEditable: false,
//                prepareAndDisplayOrderReviewData: prepareAndDisplayOrderReviewData.GetValueOrDefault());
//            return View(model);
//        }
//    }
//}
