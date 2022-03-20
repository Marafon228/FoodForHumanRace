namespace FoodForHumanRaceAdminDesktop.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UsersAndEnterprise")]
    public partial class UsersAndEnterprise
    {
        
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdEnterprise { get; set; }

        public virtual Enterprise Enterprise { get; set; }

        public virtual User User { get; set; }

       


    }
}
