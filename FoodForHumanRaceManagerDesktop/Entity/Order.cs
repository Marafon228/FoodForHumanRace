namespace FoodForHumanRaceManagerDesktop.Entity
{
    using FoodForHumanRaceManagerDesktop.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order : ObservableObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderAndProduct = new HashSet<OrderAndProduct>();
            OrderDelivery = new HashSet<OrderDelivery>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int IdUser { get; set; }

        public int? IdStatus { get; set; }

        [Column(TypeName = "money")]
        public decimal OverPrice { get; set; }

        public int Count { get; set; }

        public int? Staff { get; set; }

        public virtual Status Status { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderAndProduct> OrderAndProduct { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDelivery> OrderDelivery { get; set; }
    }
}
