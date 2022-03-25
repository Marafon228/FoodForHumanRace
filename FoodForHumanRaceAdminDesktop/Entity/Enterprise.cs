namespace FoodForHumanRaceAdminDesktop.Entity
{
    using FoodForHumanRaceAdminDesktop.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enterprise")]
    public partial class Enterprise : ObservableObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Enterprise()
        {
            TypesOfProducts = new HashSet<TypesOfProducts>();
            UsersAndEnterprise = new HashSet<UsersAndEnterprise>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int IdType { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public virtual TypeOfEnterprise TypeOfEnterprise { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TypesOfProducts> TypesOfProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersAndEnterprise> UsersAndEnterprise { get; set; }


        [NotMapped]
        public string UserLogin
        {
            get
            {

                var Login = "Login menedger" /*ADO.Instance.UsersAndEnterprise.Find().User.Login*/;
                return Login;
            }
        }
    }
}
