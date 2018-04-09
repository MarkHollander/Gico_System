using System;
using Gico.Common;
using Gico.Config;
using Gico.FrontEndModels.Models;
using Gico.OrderCommands;
using Gico.ReadCartModels;
using Gico.SystemCommands;

namespace Gico.FrontEndAppService.Mapping
{
    public static class CartItemMapping
    {
        public static CartItemAddCommand ToCommand(this CartItemChangeViewModel model, decimal price, string languageId, string createdUid)
        {
            if (model == null) return null;
            return new CartItemAddCommand(SystemDefine.DefaultVersion)
            {
                Price = price,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                LanguageId = languageId,
                CreatedUid = createdUid,
                StoreId = ConfigSettingEnum.StoreId.GetConfig(),
            };
        }

        public static CartItemChangeCommand ToCommand(this CartItemChangeViewModel model, decimal price, string languageId, string updatedUid, RCart cart)
        {
            if (model == null) return null;
            return new CartItemChangeCommand(cart.Version)
            {
                Price = price,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                LanguageId = languageId,
                CartCode = cart.Code,
                CartId = cart.Id,
                StoreId = ConfigSettingEnum.StoreId.GetConfig(),
                UpdatedUid = updatedUid,
                Action = (EnumDefine.CartActionEnum)model.Action,
                ShardId = cart.ShardId,
                Version = cart.Version,
                CreatedDateUtc = Extensions.GetCurrentDateUtc(),
            };
        }

        public static CartItemViewModel ToModel(this RCartItem model, RCartItemDetail cartItemDetail, int quantity)
        {
            if (model == null) return null;
            return new CartItemViewModel()
            {
                ProductId = model.ProductId,
                Name = cartItemDetail?.Name,
                Quantity = quantity
            };
        }
        public static CartItemViewModel ToModel(this Tuple<RCartItem, RCartItemDetail, int> item)
        {
            if (item == null) return null;
            return new CartItemViewModel()
            {
                ProductId = item.Item1.ProductId,
                Name = item.Item2?.Name,
                Quantity = item.Item3
            };
        }
        public static CheckoutItemViewModel ToCheckout(this RCartItem item, RCartItemDetail cartItemDetail, int quantity)
        {
            if (item == null) return null;
            return new CheckoutItemViewModel()
            {
                ProductId = item.ProductId,
                Name = cartItemDetail?.Name,
                Price = item.Price,
                Quantity = quantity
            };
        }
        public static CheckoutItemViewModel ToCheckout(this Tuple<RCartItem, RCartItemDetail, int> item)
        {
            if (item == null) return null;
            return new CheckoutItemViewModel()
            {
                ProductId = item.Item1.ProductId,
                Name = item.Item2?.Name,
                Price = item.Item1.Price,
                Quantity = item.Item3
            };
        }
    }
}