namespace WebApplicationFoodForHumanRace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderAndProduct")]
    public partial class OrderAndProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int IdOrder { get; set; }

        public int IdProduct { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
