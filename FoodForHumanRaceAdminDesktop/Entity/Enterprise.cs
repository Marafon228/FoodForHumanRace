namespace FoodForHumanRaceAdminDesktop.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enterprise")]
    public partial class Enterprise
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Enterprise()
        {
            TypesOfProducts = new HashSet<TypesOfProducts>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int IdType { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        public int IdUser { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public virtual TypeOfEnterprise TypeOfEnterprise { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TypesOfProducts> TypesOfProducts { get; set; }
    }
}
