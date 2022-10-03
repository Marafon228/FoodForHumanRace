using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationFoodForHumanRace.Models.Json
{
    public class GetEnterpriseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdType { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }


    }
}