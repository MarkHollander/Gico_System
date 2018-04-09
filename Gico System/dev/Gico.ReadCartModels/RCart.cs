using System;
using System.Collections.Generic;
using System.Linq;
using Gico.Config;
using ProtoBuf;

namespace Gico.ReadCartModels
{
    [ProtoContract]
    public class RCart : BaseReadModel
    {
        [ProtoMember(1)]
        public string ClientId { get; set; }
        [ProtoMember(2)]
        public new EnumDefine.CartStatusEnum Status { get; set; }
        [ProtoMember(3)]
        public RCartItem[] CartItems { get; set; }
        [ProtoMember(4)]
        public RCartItemDetail[] CartItemDetails { get; set; }

        public Tuple<RCartItem, RCartItemDetail, int>[] CartItemFulls
        {
            get
            {
                Tuple<RCartItem, RCartItemDetail, int>[] items = CartItems.GroupBy(p => new { p.ProductId, p.Price })
                    .Select(p =>
                    new Tuple<RCartItem, RCartItemDetail, int>(
                        p.First(),
                        CartItemDetails.FirstOrDefault(q => q.ProductId == p.Key.ProductId),
                        p.Count()
                        )).ToArray();
                return items;
            }
        }
    }
    [ProtoContract]
    public class RCartItem : BaseReadModel
    {
        [ProtoMember(1)]
        public string ProductId { get; set; }
        [ProtoMember(2)]
        public decimal Price { get; set; }
        [ProtoMember(3)]
        public new EnumDefine.CartStatusEnum Status { get; set; }
        public DateTime PickupTime { get; set; }
        public DateTime ShippingTime { get; set; }

    }
    [ProtoContract]
    public class RCartItemDetail : BaseReadModel
    {
        [ProtoMember(1)]
        public string ProductId { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
    }
}
