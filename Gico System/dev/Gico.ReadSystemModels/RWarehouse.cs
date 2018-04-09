using System;
using System.Collections.Generic;
using System.Linq;

namespace Gico.ReadSystemModels
{
    public class RWarehouse
    {
        public string WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int WarehouseStatus { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int VillageId { get; set; }
        public int RoadId { get; set; }
        public string AddressDetail { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int WarehouseType { get; set; }
        public WorkingTime[] WorkingTimes { get; set; }
        public Holiday[] HolidayTimes { get; set; }
    }

    public class WorkingTime
    {
        public DayOfWeek DayOfWeek { get; set; }
        public Tuple<int, int>[] Times { get; set; }
    }

    public class Holiday
    {
        public DateTime Day { get; set; }
        public Tuple<int, int>[] HolidayTimes { get; set; }

        public Tuple<int, int>[] WorkingTimes
        {
            get
            {
                int startMinute = 0;
                int endMinute = 1439;
                IList<Tuple<int, int>> tuples = new List<Tuple<int, int>>();
                foreach (var holidayTime in HolidayTimes)
                {
                    if (startMinute < holidayTime.Item1)
                    {
                        tuples.Add(new Tuple<int, int>(startMinute, holidayTime.Item1));
                        startMinute = holidayTime.Item2;
                    }
                }
                if (tuples[tuples.Count].Item2 < endMinute)
                {
                    tuples.Add(new Tuple<int, int>(tuples[tuples.Count].Item2, endMinute));
                }
                return tuples.ToArray();
            }
        }
    }

}