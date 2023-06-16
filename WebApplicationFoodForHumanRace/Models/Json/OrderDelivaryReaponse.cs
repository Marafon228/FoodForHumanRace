using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationFoodForHumanRace.Models.Json
{
    public class OrderDelivaryReaponse
    {
        public decimal? LatitudeKl { get; set; }

        public decimal? LongitudeKl { get; set; }

        public decimal? LatitudeStaff { get; set; }

        public decimal? LongitudeStaff { get; set; }
    }
}