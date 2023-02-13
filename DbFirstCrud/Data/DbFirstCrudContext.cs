using System;
using System.Collections.Generic;
using DbFirstCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace DbFirstCrud.Data;

public partial class DbFirstCrudContext : DbContext
{
    public DbFirstCrudContext()
    {
    }

    public DbFirstCrudContext(DbContextOptions<DbFirstCrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-J8PE8SK\\SQLEXPRESS;Database=DbFirstCrud;user=sa;password=test@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CtCode).HasName("PK__Categori__01A6D04E19E706A5");

            entity.Property(e => e.CtCode).IsFixedLength();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D809210072");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__customer__A25C5AA664E19E3F");

            entity.Property(e => e.Category).IsFixedLength();

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Products).HasConstraintName("FK__customers__Categ__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
