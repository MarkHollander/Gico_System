using System;
using System.Collections.Generic;

namespace Gico.ReadOrderModels.Giftcodes
{
    public class RGiftCodeCalendar
    {
        public DateTime Date { get; set; }
        public List<int> Times { get; set; }
    }
}