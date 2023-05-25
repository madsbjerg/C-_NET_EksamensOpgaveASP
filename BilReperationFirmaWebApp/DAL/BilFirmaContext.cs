using BilReperationFirmaWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Type = BilReperationFirmaWebApp.Models.Type;

namespace BilReperationFirmaWebApp.DAL
{
    public class BilFirmaContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }

        public BilFirmaContext(DbContextOptions<BilFirmaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Mechanic>().HasKey(m => m.Id);
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Type>().HasKey(t => t.Id);
            modelBuilder.Entity<OrderType>().HasKey(ot => new { ot.OrderId, ot.TypeId });

            modelBuilder.Entity<OrderType>()
                .HasOne(ot => ot.Order)
                .WithMany(o => o.OrderTypes)
                .HasForeignKey(ot => ot.OrderId);

            modelBuilder.Entity<OrderType>()
                .HasOne(ot => ot.Type)
                .WithMany(t => t.OrderTypes)
                .HasForeignKey(ot => ot.TypeId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Mechanic)
                .WithMany(m => m.Orders)
                .HasForeignKey(o => o.MechanicId);

            modelBuilder.Entity<Customer>().HasData(new Customer[]
            {
                new Customer{Id = -1, Name = "John Doe", Phonenumber = "+45 12 34 56 78", SignUpDate = DateTime.Now},
                new Customer{Id = -2, Name = "Linse Testperson", Phonenumber = "+45 87 65 43 21", SignUpDate = DateTime.Now},
                new Customer{Id = -3, Name = "Johan Jakobsen", Phonenumber = "+45 10 09 08 07", SignUpDate = DateTime.Now}
            });

            modelBuilder.Entity<Mechanic>().HasData(new Mechanic[]
            {
                new Mechanic{Id = -1, Name = "Mechanic #1", Salary = 2000.50, HiringDate = DateTime.Now},
                new Mechanic{Id = -2, Name = "Mechanic #2", Salary = 2500.50, HiringDate = DateTime.Now},
                new Mechanic{Id = -3, Name = "Mechanic #3", Salary = 1249.99, HiringDate = DateTime.Now}
            });

            modelBuilder.Entity<Order>().HasData(new Order[]
            {
                new Order{Id = -1, Price = 100, IsFinished = false, CustomerId = -1, MechanicId = -1},
                new Order{Id = -2, Price = 150, IsFinished = false, CustomerId = -1, MechanicId = -2},
                new Order{Id = -3, Price = 200, IsFinished = true, CustomerId = -2, MechanicId = -2},
                new Order{Id = -4, Price = 250, IsFinished = true, CustomerId = -3, MechanicId = -2},
                new Order{Id = -5, Price = 300, IsFinished = false, CustomerId = -2, MechanicId = -3}
            });

            modelBuilder.Entity<Type>().HasData(new Type[]
            {
                new Type{Id = -1, Description = "Inspection"},
                new Type{Id = -2, Description = "Oil-check"},
                new Type{Id = -3, Description = "Tire Change"},
                new Type{Id = -4, Description = "Battery replacement"}
            });

            modelBuilder.Entity<OrderType>().HasData(new OrderType[]
            {
                new OrderType{OrderId = -1, TypeId = -1},
                new OrderType{OrderId = -2, TypeId = -1},
                new OrderType{OrderId = -2, TypeId = -2},
                new OrderType{OrderId = -3, TypeId = -3},
                new OrderType{OrderId = -3, TypeId = -4},
                new OrderType{OrderId = -4, TypeId = -4},
                new OrderType{OrderId = -5, TypeId = -2}
            });
        }
    }
}
