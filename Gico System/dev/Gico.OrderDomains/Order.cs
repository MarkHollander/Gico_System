using System;
using System.Collections.Generic;
using Gico.CQRS.Model.Interfaces;
using Gico.Domains;

namespace Gico.OrderDomains
{
    public class Order : BaseDomain,IVersioned
    {
        public Order(int version)
        {
            Version = version;
        }

        public string CustomerId { get; set; }
        public string BillingAddressId { get; set; }
        public string ShippingAddressId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyRate { get; set; }
        public string CustomerIp { get; set; }
        public string TaxCode { get; set; }
        public string TaxCompanyName { get; set; }
        public string TaxAddress { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public List<PaidAdvance> PaidAdvances { get; set; }
        public List<DeliveryRequired> DeliveryRequireds { get; set; }
        public int Version { get; }
    }
}
