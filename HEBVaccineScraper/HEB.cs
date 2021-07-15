using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEBVaccineScraper
{
    public class HEB
    {
        public const string URL = "https://heb-ecom-covid-vaccine.hebdigital-prd.com/vaccine_locations.json";

        public class SlotDetail
        {
            public int openTimeslots { get; set; }
            public int openAppointmentSlots { get; set; }
            public string manufacturer { get; set; }
        }

        public class Location
        {
            public string zip { get; set; }
            public string url { get; set; }
            public string type { get; set; }
            public string street { get; set; }
            public int? storeNumber { get; set; }
            public string state { get; set; }
            public List<SlotDetail> slotDetails { get; set; }
            public int openTimeslots { get; set; }
            public int openAppointmentSlots { get; set; }
            public string name { get; set; }
            public double? longitude { get; set; }
            public double? latitude { get; set; }
            public string city { get; set; }

            public string GetAddress()
            {
                return $"{street}\n{city}, {state} {zip}";
            }
        }

        public class Root
        {
            public List<Location> locations { get; set; }
        }
    }
}
