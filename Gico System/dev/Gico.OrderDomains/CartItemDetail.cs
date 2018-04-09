using System;
using Gico.Domains;
using Gico.OrderCommands;
using Gico.OrderEvents.Cache;
using Gico.ReadCartModels;

namespace Gico.OrderDomains
{
    public class CartItemDetail : BaseDomain
    {
        public CartItemDetail(RCartItemDetail cartItemDetail)
        {
            Id = cartItemDetail.Id;
            ShardId = cartItemDetail.ShardId;
            CreatedDateUtc = cartItemDetail.CreatedDateUtc;
            UpdatedDateUtc = cartItemDetail.UpdatedDateUtc;
            CreatedUid = cartItemDetail.CreatedUid;
            UpdatedUid = cartItemDetail.UpdatedUid;
            LanguageId = cartItemDetail.LanguageId;
            ProductId = cartItemDetail.ProductId;
            Name = cartItemDetail.Name;
        }
        public CartItemDetail(CartItemDetailAddCommand command)
        {
            Id = Common.Common.GenerateGuid();
            ProductId = command.ProductId;
            Name = command.Name;
        }
        public CartItemDetail(CartItemDetailAddCommand command, string languageId, string createdUid)
        {
            Id = Common.Common.GenerateGuid();
            ProductId = command.ProductId;
            Name = command.Name;
            LanguageId = languageId;
            CreatedUid = createdUid;
        }

        #region Event

        public void CreateAddItemToCartEvent()
        {
            var @event = ToAddOrChangeEvent();
            AddEvent(@event);
        }

        #endregion

        public string ProductId { get; set; }
        public string Name { get; set; }

        #region Convert

        internal CartItemDetailCacheAddOrChangeEvent ToAddOrChangeEvent()
        {
            return new CartItemDetailCacheAddOrChangeEvent()
            {
                Name = this.Name,
                ProductId = this.ProductId,
            };
        }

        #endregion

    }
}