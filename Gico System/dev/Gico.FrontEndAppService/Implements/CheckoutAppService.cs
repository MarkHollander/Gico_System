using System.Linq;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.CQRS.Model.Implements;
using Gico.FrontEndAppService.Interfaces;
using Gico.FrontEndAppService.Mapping;
using Gico.FrontEndModels.Models;
using Gico.OrderCommands;
using Gico.OrderService.Interfaces;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Gico.FrontEndAppService.Implements
{
    public class CheckoutAppService : PageAppService, ICheckoutAppService
    {
        private readonly IAddressService _addressService;
        private readonly ICartService _cartService;
        private readonly ILocationService _locationService;
        public CheckoutAppService(IMenuService menuService,
            ILocaleStringResourceCacheStorage localeStringResourceCacheStorage,
            IAddressService addressService,
            ICartService cartService,
            ILocationService locationService,
            ILogger<CheckoutAppService> logger,
            ICurrentContext currentContext) : base(menuService, localeStringResourceCacheStorage, currentContext, logger)
        {
            _addressService = addressService;
            _cartService = cartService;
            _locationService = locationService;
        }
        public async Task<CheckoutViewModel> Checkout()
        {
            bool isLogin = await _currentContext.IsAuthenticated();
            CheckoutViewModel model = new CheckoutViewModel(await InitPage())
            {
                IsLogin = isLogin,
                ClientId = _currentContext.ClientId
            };
            if (isLogin)
            {
                var customer = await _currentContext.GetCurrentCustomer();
                var oldAddresses = await _addressService.GetByCustomerId(customer.Id, 0, 3);
                model.Addresses = oldAddresses?.Select(p => p.ToModel()).ToArray();
            }
            model.DeliveryTimes = new DeliveryTime[0];
            var cart = await _cartService.GetFromCache(_currentContext.ClientId);
            if (cart != null)
            {
                model.CheckoutItems = cart.CartItemFulls.Select(p => p.ToCheckout()).ToArray();
            }
            return model;
        }

        public async Task<LocationViewModel[]> LocationSearch()
        {
            var locations = await _locationService.Search(_currentContext.GetParam("q"));
            LocationViewModel[] model = locations?.Select(p => p.ToModel()).ToArray();
            return model;
        }

        public async Task<CheckoutViewModel> AddressSelected(AddressSelectedViewModel model)
        {
            var cart = await _cartService.GetFromCache(_currentContext.ClientId);

            AddressSelectedCommand command = new AddressSelectedCommand(cart.Version)
            {
                CartId = cart.Id,
                DistrictId = model.DistrictId,
                ProvinceId = model.ProvinceId,
                StreetId = model.StreetId,
                WardId = model.WardId,
                ShardId = cart.ShardId
            };
            CommandResult result = await _cartService.AddressSelected(command);
            bool isLogin = await _currentContext.IsAuthenticated();
            CheckoutViewModel modelResponse = new CheckoutViewModel(await InitPage())
            {
                IsLogin = isLogin,
                ClientId = _currentContext.ClientId
            };
            if (result.IsSucess)
            {
                cart = await _cartService.GetFromCache(_currentContext.ClientId);
                if (cart != null)
                {
                    modelResponse.CheckoutItems = cart.CartItemFulls.Select(p => p.ToCheckout()).ToArray();
                }
            }
            else
            {
                modelResponse.AddMessage(result.ResourceName);
            }
            return modelResponse;
        }
    }
}