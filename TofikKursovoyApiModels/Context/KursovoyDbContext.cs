using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TofikKursovoyApiModels;

public partial class KursovoyDbContext : DbContext
{
    public KursovoyDbContext()
    {
    }

    public KursovoyDbContext(DbContextOptions<KursovoyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productorder> Productorders { get; set; }

    public virtual DbSet<Typeoftechnic> Typeoftechnics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("server=localhost;user id=postgres;password=1234;database=KursovoyDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brand_pkey");

            entity.ToTable("brand");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Surname).HasColumnName("surname");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("discount_pkey");

            entity.ToTable("discount");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DiscountCost).HasColumnName("discount_cost");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("to_product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");

            entity.ToTable("orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dateorder).HasColumnName("dateorder");
            entity.Property(e => e.IdClient).HasColumnName("id_client");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("to_client");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brandname).HasColumnName("brandname");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Existance).HasColumnName("existance");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Photoname).HasColumnName("photoname");
            entity.Property(e => e.ShortDescription).HasColumnName("short_description");
            entity.Property(e => e.Typeoftechnic).HasColumnName("typeoftechnic");

            entity.HasOne(d => d.BrandnameNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Brandname)
                .HasConstraintName("to_brand");

            entity.HasOne(d => d.TypeoftechnicNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Typeoftechnic)
                .HasConstraintName("product_to_typeoftechnic");
        });

        modelBuilder.Entity<Productorder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productorder_pkey");

            entity.ToTable("productorder");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.Productorders)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("to_order");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Productorders)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("to_product");
        });

        modelBuilder.Entity<Typeoftechnic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("typeoftechnic_pkey");

            entity.ToTable("typeoftechnic");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
