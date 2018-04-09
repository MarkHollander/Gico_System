using System;
using System.Linq;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.Config;
using Gico.FrontEndAppService.Interfaces;
using Gico.FrontEndAppService.Mapping;
using Gico.FrontEndModels.Models;
using Gico.OrderCommands;
using Gico.OrderDomains;
using Gico.OrderService.Interfaces;
using Gico.ReadCartModels;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Gico.FrontEndAppService.Implements
{
    public class CartAppService : PageAppService, ICartAppService
    {
        private readonly ICartService _cartService;
        private readonly ICommonService _commonService;

        public CartAppService(IMenuService menuService,
            ILocaleStringResourceCacheStorage localeStringResourceCacheStorage,
            ILogger<CartAppService> logger,
            ICartService cartService,
            ICommonService commonService, ICurrentContext currentContext) : base(menuService, localeStringResourceCacheStorage, currentContext, logger)
        {
            _cartService = cartService;
            _commonService = commonService;
        }

        public async Task<CartViewModel> Get()
        {
            try
            {
                CartViewModel model = new CartViewModel(await InitAjax());
                RCart cart = await _cartService.GetFromCache(_currentContext.ClientId, EnumDefine.CartStatusEnum.New);
                if (cart != null)
                {
                    model.CartItems = cart.CartItemFulls?.Select(p => p.ToModel()).ToArray();
                }
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }

        public async Task<CartViewModel> Change(CartItemChangeViewModel item)
        {
            try
            {
                var customer = await _currentContext.GetCurrentCustomer();
                CartViewModel model = new CartViewModel(await InitAjax());
                RCart cart = await _cartService.GetFromCache(_currentContext.ClientId, EnumDefine.CartStatusEnum.New);
                if (cart == null)
                {
                    //int shardId= _commonService.get

                    var command = item.ToCommand(1, _currentContext.LanguageId, customer.Id);
                    long systemIdentity = await _commonService.GetNextId(typeof(Cart));
                    CartAddCommand cartAddCommand = new CartAddCommand(SystemDefine.DefaultVersion)
                    {
                        LanguageId = _currentContext.LanguageId,
                        Code = Common.Common.GenerateCodeFromId(systemIdentity),
                        StoreId = ConfigSettingEnum.StoreId.GetConfig(),
                        ClientId = _currentContext.ClientId,
                        CreatedUid = customer.Id ?? string.Empty,
                        CartItem = command,
                        CartItemDetail = new CartItemDetailAddCommand(SystemDefine.DefaultVersion)
                        {
                            ProductId = command.ProductId,
                            Name = "Name",

                        }
                    };
                    await _cartService.Add(cartAddCommand);
                }
                else
                {
                    //if (cart.Version != item.Version)
                    //{
                    //    model.AddMessage(ResourceKey.Cart_IsChanged);
                    //    return model;
                    //}
                    CartItemChangeCommand command = item.ToCommand(1, _currentContext.LanguageId, customer.Id, cart);
                    await _cartService.Change(command);
                }
                cart = await _cartService.GetFromCache(_currentContext.ClientId, EnumDefine.CartStatusEnum.New);
                model.CartItems = cart.CartItemFulls?.Select(p => p.ToModel()).ToArray();
                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }

        }


    }
}