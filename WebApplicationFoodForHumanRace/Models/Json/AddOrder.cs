using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationFoodForHumanRace.Models.Json
{
    public class AddOrder
    {
        public string Description { get; set; }
        public string LoginUser { get; set; }
        public ManuProduct[] ManuProducts { get; set; }
    }

    public class ManuProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }

    }
}