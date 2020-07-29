using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaStore.Storing
{
    public partial class PizzaStoreDbContext : DbContext
    {
        public PizzaStoreDbContext()
        {
        }

        public PizzaStoreDbContext(DbContextOptions<PizzaStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Crust> Crust { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<PizzaTopping> PizzaTopping { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Toppings> Toppings { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=localhost;database=PizzaStoreDb;user id=sa;password=Password12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crust>(entity =>
            {
                entity.ToTable("Crust", "Pizza");

                entity.Property(e => e.CrustId).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_OrderId");

                entity.ToTable("Orders", "Orders");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.StoreSubmitted)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreSubmittedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreSubmittedId");

                entity.HasOne(d => d.UserSubmitted)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserSubmittedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSubmittedId");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("Pizza", "Pizza");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Crust)
                    .WithMany(p => p.Pizza)
                    .HasForeignKey(d => d.CrustId)
                    .HasConstraintName("FK_CrustId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Pizza)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderId");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Pizza)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK_SizeId");
            });

            modelBuilder.Entity<PizzaTopping>(entity =>
            {
                entity.ToTable("PizzaTopping", "Pizza");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzaTopping)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PizzaId");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.PizzaTopping)
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToppingId");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size", "Pizza");

                entity.Property(e => e.SizeId).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store", "Store");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Toppings>(entity =>
            {
                entity.HasKey(e => e.ToppingId)
                    .HasName("PK_ToppingId");

                entity.ToTable("Toppings", "Pizza");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserId");

                entity.ToTable("Users", "Users");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
