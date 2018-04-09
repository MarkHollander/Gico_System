using System;
using System.Collections.Generic;

namespace Gico.OrderDomains.Giftcodes
{
    public class GiftCodeCalendar
    {
        public GiftCodeCalendar(DateTime date, int[] times)
        {
            Date = date;
            Times = times;
        }
        public DateTime Date { get; private set; }
        public int[] Times { get; private set; }
    }
}