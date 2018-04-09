using System;

namespace Gico.FrontEndModels.Models
{
    public class DeliveryTime
    {
        public string PoCode { get; set; }
        public CheckoutItemViewModel[] Items { get; set; }
        public Tuple<DateTime, DateTime> Minutes { get; set; }

        public string DisplayText { get; set; }
    }
}