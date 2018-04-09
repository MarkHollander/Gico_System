using System.Linq;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.OrderCacheStorage.Interfaces;
using Gico.OrderCommands;
using Gico.OrderDataObject.Interfaces;
using Gico.OrderDomains;
using Gico.OrderService.Interfaces;
using Gico.ReadCartModels;

namespace Gico.OrderService.Implements
{
    public class CartService : ICartService
    {
        private readonly ICommandSender _commandService;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartItemDetailRepository _cartItemDetailRepository;
        private readonly ICartCacheStorage _cartCacheStorage;
        public CartService(ICartRepository cartRepository, ICommandSender commandService, ICartItemRepository cartItemRepository, ICartCacheStorage cartCacheStorage, ICartItemDetailRepository cartItemDetailRepository)
        {
            _cartRepository = cartRepository;
            _commandService = commandService;
            _cartItemRepository = cartItemRepository;
            _cartCacheStorage = cartCacheStorage;
            _cartItemDetailRepository = cartItemDetailRepository;
        }

        public async Task<RCart> GetFromDb(string connectionString, string id)
        {
            var cart = await _cartRepository.Get(connectionString, id);
            cart.CartItems = await _cartItemRepository.Get(connectionString, cart.Id, EnumDefine.CartStatusEnum.New);
            cart.CartItemDetails = await _cartItemDetailRepository.Get(connectionString, cart.Id);
            return cart;
        }
        public async Task<RCart> GetCurrentCartFromDb(string connectionString, string clientId)
        {
            var cart = await _cartRepository.Get(connectionString, clientId, EnumDefine.CartStatusEnum.New);
            cart.CartItems = await _cartItemRepository.Get(connectionString, cart.Id, EnumDefine.CartStatusEnum.New);
            cart.CartItemDetails = await _cartItemDetailRepository.Get(connectionString, cart.Id);
            return cart;
        }

        public async Task<RCart> GetFromCache(string clientId)
        {
            var cart = await _cartCacheStorage.Get(clientId);
            return cart;
        }

        public async Task<RCart> GetFromCache(string clientId, EnumDefine.CartStatusEnum status)
        {
            var cart = await GetFromCache(clientId);
            if (cart != null)
            {
                if (cart.Status == status)
                {
                    cart.CartItems = cart.CartItems.Where(p => p.Status == status).ToArray();
                }
                else
                {
                    return null;
                }
            }
            return cart;
        }

        public async Task<CommandResult> Add(CartAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<CommandResult> Change(CartItemChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task<bool> CreatingCart(string clientId)
        {
            return await _cartCacheStorage.CreatingCart(clientId);
        }

        public async Task<bool> CreatedCart(string clientId)
        {
            return await _cartCacheStorage.CreatedCart(clientId);
        }

        public async Task Save(string connectionString, Cart cart, CartItem[] cartItems,
            CartItemDetail cartItemDetail)
        {
            await _cartRepository.Save(connectionString, cart, cartItems, cartItemDetail, _cartItemRepository.Add, _cartItemDetailRepository.Add);
        }

        public async Task Save(string connectionString, int id, string cartId, string cartCode, int version, CartItem[] cartItems,
            CartItemDetail shoppingCartItemDetail)
        {
            await _cartItemRepository.Add(connectionString, id, cartId, cartCode, version, cartItems, shoppingCartItemDetail, _cartItemDetailRepository.Add, _cartRepository.Change);
        }

        public async Task Remove(string connectionString, string cartId, int version, string[] cartItemIds, string[] cartItemDetailIds)
        {
            await _cartItemRepository.Remove(connectionString, cartId, version, cartItemIds, cartItemDetailIds,
                _cartRepository.Change, _cartItemDetailRepository.Remove);
        }

        public async Task<CommandResult> AddressSelected(AddressSelectedCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        
    }
}
