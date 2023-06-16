namespace WebApplicationFoodForHumanRace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDelivery")]
    public partial class OrderDelivery
    {
        public int Id { get; set; }

        public decimal? LatitudeKl { get; set; }

        public decimal? LongitudeKl { get; set; }

        public decimal? LatitudeStaff { get; set; }

        public decimal? LongitudeStaff { get; set; }

        public int IdOrder { get; set; }

        public int? IdStaff { get; set; }

        public bool IsActive { get; set; }

        public virtual Order Order { get; set; }

        public virtual User User { get; set; }
    }
}
