using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplicationFoodForHumanRace.Models
{
    public partial class ADO : DbContext
    {
        public ADO()
            : base("name=ADO")
        {
        }
        private static ADO _instance;
        public static ADO Instance
        {
            get { return _instance ?? (_instance = new ADO()); }
        }

        public virtual DbSet<Enterprise> Enterprise { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderAndProduct> OrderAndProduct { get; set; }
        public virtual DbSet<OrderDelivery> OrderDelivery { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TypeOfEnterprise> TypeOfEnterprise { get; set; }
        public virtual DbSet<TypesOfProducts> TypesOfProducts { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UsersAndEnterprise> UsersAndEnterprise { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enterprise>()
                .Property(e => e.Latitude)
                .HasPrecision(11, 8);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.Longitude)
                .HasPrecision(11, 8);

            modelBuilder.Entity<Enterprise>()
                .HasMany(e => e.TypesOfProducts)
                .WithRequired(e => e.Enterprise)
                .HasForeignKey(e => e.IdEnterprise)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Enterprise>()
                .HasMany(e => e.UsersAndEnterprise)
                .WithRequired(e => e.Enterprise)
                .HasForeignKey(e => e.IdEnterprise)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OverPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderAndProduct)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.IdOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDelivery)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.IdOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDelivery>()
                .Property(e => e.LatitudeKl)
                .HasPrecision(11, 8);

            modelBuilder.Entity<OrderDelivery>()
                .Property(e => e.LongitudeKl)
                .HasPrecision(11, 8);

            modelBuilder.Entity<OrderDelivery>()
                .Property(e => e.LatitudeStaff)
                .HasPrecision(11, 8);

            modelBuilder.Entity<OrderDelivery>()
                .Property(e => e.LongitudeStaff)
                .HasPrecision(11, 8);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderAndProduct)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.TypesOfProducts)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.IdRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Order)
                .WithOptional(e => e.Status)
                .HasForeignKey(e => e.IdStatus);

            modelBuilder.Entity<TypeOfEnterprise>()
                .HasMany(e => e.Enterprise)
                .WithRequired(e => e.TypeOfEnterprise)
                .HasForeignKey(e => e.IdType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.OrderDelivery)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.IdStaff);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UsersAndEnterprise)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);
        }
    }
}
