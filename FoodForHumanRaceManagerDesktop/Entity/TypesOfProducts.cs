namespace FoodForHumanRaceManagerDesktop.Entity
{
    using FoodForHumanRaceManagerDesktop.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TypesOfProducts : ObservableObject
    {
        public int Id { get; set; }

        public int IdProduct { get; set; }

        public int IdEnterprise { get; set; }

        public virtual Enterprise Enterprise { get; set; }

        public virtual Product Product { get; set; }
    }
}
