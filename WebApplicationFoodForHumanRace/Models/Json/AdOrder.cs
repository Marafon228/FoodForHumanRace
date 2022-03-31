using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationFoodForHumanRace.Models.Json
{
    public class AdOrder
    {
        public string Description { get; set; }
        public string LoginUser { get; set; }
        public int ProductId { get; set; }
    }
}