namespace FoodForHumanRaceAdminDesktop.Entity
{
    using FoodForHumanRaceAdminDesktop.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderAndProduct")]
    public partial class OrderAndProduct : ObservableObject
    {

        public int Id { get; set; }

        public int IdOrder { get; set; }

        public int IdProduct { get; set; }

        public int Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
