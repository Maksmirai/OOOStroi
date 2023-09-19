using System;
using System.Collections.Generic;
using DemoStroi.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoStroi.Context;

public partial class User003Context : DbContext
{
    private static User003Context _context;

    public static User003Context GetContext()
    {
        if (_context == null)
            _context = new User003Context();
        return _context;
    }
    
    public User003Context()
    {
    }

    public User003Context(DbContextOptions<User003Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoryproduct> Categoryproducts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderproduct> Orderproducts { get; set; }

    public virtual DbSet<Pickuppoint> Pickuppoints { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productmanufacturer> Productmanufacturers { get; set; }

    public virtual DbSet<Productsupplier> Productsuppliers { get; set; }

    public virtual DbSet<Productunit> Productunits { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseNpgsql("Host=192.168.2.159;Database=user003;Username=user003;password=67506");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoryproduct>(entity =>
        {
            entity.HasKey(e => e.Categoryproductid).HasName("categoryproduct_pkey");

            entity.ToTable("categoryproduct");

            entity.Property(e => e.Categoryproductid)
                .ValueGeneratedNever()
                .HasColumnName("categoryproductid");
            entity.Property(e => e.Categoryproductname)
                .HasMaxLength(100)
                .HasColumnName("categoryproductname");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("Order_pkey");

            entity.ToTable("Order");

            entity.Property(e => e.Orderid)
                .ValueGeneratedNever()
                .HasColumnName("orderid");
            entity.Property(e => e.Ordercode).HasColumnName("ordercode");
            entity.Property(e => e.Orderdate).HasColumnName("orderdate");
            entity.Property(e => e.Orderdeliverydate).HasColumnName("orderdeliverydate");
            entity.Property(e => e.Orderpickuppoint).HasColumnName("orderpickuppoint");
            entity.Property(e => e.Orderstatus)
                .HasColumnType("bit(1)")
                .HasColumnName("orderstatus");
            entity.Property(e => e.Orderusername).HasColumnName("orderusername");

            entity.HasOne(d => d.OrderpickuppointNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Orderpickuppoint)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_orderpickuppoint_fkey");

            entity.HasOne(d => d.OrderusernameNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Orderusername)
                .HasConstraintName("Order_orderusername_fkey");
        });

        modelBuilder.Entity<Orderproduct>(entity =>
        {
            entity.HasKey(e => new { e.Orderid, e.Productarticlenumber }).HasName("orderproduct_pkey");

            entity.ToTable("orderproduct");

            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Productarticlenumber)
                .HasMaxLength(100)
                .HasColumnName("productarticlenumber");
            entity.Property(e => e.Productcount).HasColumnName("productcount");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderproducts)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderproduct_orderid_fkey");

            entity.HasOne(d => d.ProductarticlenumberNavigation).WithMany(p => p.Orderproducts)
                .HasForeignKey(d => d.Productarticlenumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderproduct_productarticlenumber_fkey");
        });

        modelBuilder.Entity<Pickuppoint>(entity =>
        {
            entity.HasKey(e => e.Pickeppointid).HasName("pickuppoint_pkey");

            entity.ToTable("pickuppoint");

            entity.Property(e => e.Pickeppointid)
                .ValueGeneratedNever()
                .HasColumnName("pickeppointid");
            entity.Property(e => e.Pickuppointaddress)
                .HasMaxLength(100)
                .HasColumnName("pickuppointaddress");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productarticlenumber).HasName("product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.Productarticlenumber)
                .HasMaxLength(100)
                .HasColumnName("productarticlenumber");
            entity.Property(e => e.Productcategory).HasColumnName("productcategory");
            entity.Property(e => e.Productcost)
                .HasPrecision(19, 4)
                .HasColumnName("productcost");
            entity.Property(e => e.Productdescription)
                .HasMaxLength(200)
                .HasColumnName("productdescription");
            entity.Property(e => e.Productdiscountamount).HasColumnName("productdiscountamount");
            entity.Property(e => e.Productmanufacturer).HasColumnName("productmanufacturer");
            entity.Property(e => e.Productmaxdiscount).HasColumnName("productmaxdiscount");
            entity.Property(e => e.Productname)
                .HasMaxLength(50)
                .HasColumnName("productname");
            entity.Property(e => e.Productphoto)
                .HasMaxLength(100)
                .HasColumnName("productphoto");
            entity.Property(e => e.Productquantityinstock).HasColumnName("productquantityinstock");
            entity.Property(e => e.Productsupplier).HasColumnName("productsupplier");
            entity.Property(e => e.Productunit).HasColumnName("productunit");

            entity.HasOne(d => d.ProductcategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Productcategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_productcategory_fkey");

            entity.HasOne(d => d.ProductmanufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Productmanufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_productmanufacturer_fkey");

            entity.HasOne(d => d.ProductsupplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Productsupplier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_productsupplier_fkey");

            entity.HasOne(d => d.ProductunitNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Productunit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_productunit_fkey");
        });

        modelBuilder.Entity<Productmanufacturer>(entity =>
        {
            entity.HasKey(e => e.Productmanufacturerid).HasName("productmanufacturer_pkey");

            entity.ToTable("productmanufacturer");

            entity.Property(e => e.Productmanufacturerid)
                .ValueGeneratedNever()
                .HasColumnName("productmanufacturerid");
            entity.Property(e => e.Productmanufacturername)
                .HasMaxLength(50)
                .HasColumnName("productmanufacturername");
        });

        modelBuilder.Entity<Productsupplier>(entity =>
        {
            entity.HasKey(e => e.Productsupplierid).HasName("productsupplier_pkey");

            entity.ToTable("productsupplier");

            entity.Property(e => e.Productsupplierid)
                .ValueGeneratedNever()
                .HasColumnName("productsupplierid");
            entity.Property(e => e.Productsuppliername)
                .HasMaxLength(50)
                .HasColumnName("productsuppliername");
        });

        modelBuilder.Entity<Productunit>(entity =>
        {
            entity.HasKey(e => e.Unitproductid).HasName("productunit_pkey");

            entity.ToTable("productunit");

            entity.Property(e => e.Unitproductid)
                .ValueGeneratedNever()
                .HasColumnName("unitproductid");
            entity.Property(e => e.Unitproductname)
                .HasMaxLength(50)
                .HasColumnName("unitproductname");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.Roleid)
                .ValueGeneratedNever()
                .HasColumnName("roleid");
            entity.Property(e => e.Rolename)
                .HasMaxLength(30)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Userid)
                .ValueGeneratedNever()
                .HasColumnName("userid");
            entity.Property(e => e.Userlogin)
                .HasMaxLength(50)
                .HasColumnName("userlogin");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
            entity.Property(e => e.Userpassword)
                .HasMaxLength(50)
                .HasColumnName("userpassword");
            entity.Property(e => e.Userpatronymic)
                .HasMaxLength(100)
                .HasColumnName("userpatronymic");
            entity.Property(e => e.Userrole).HasColumnName("userrole");
            entity.Property(e => e.Usersurname)
                .HasMaxLength(100)
                .HasColumnName("usersurname");

            entity.HasOne(d => d.UserroleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Userrole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_userrole_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
