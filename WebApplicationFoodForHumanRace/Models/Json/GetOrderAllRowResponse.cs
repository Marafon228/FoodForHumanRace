using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationFoodForHumanRace.Models.Json
{
    public class GetOrderAllRowResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int IdUser { get; set; }
        public int? IdStatus { get; set; }
        public decimal OverPrice { get; set; }
        public int Count { get; set; }

    }
}