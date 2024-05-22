using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Server.Models;

public partial class CourseWorkContext : DbContext
{
    public CourseWorkContext()
    {
    }

    public CourseWorkContext(DbContextOptions<CourseWorkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AmazonCategory> AmazonCategories { get; set; }

    public virtual DbSet<AmazonProduct> AmazonProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-4AN8AIM\\SQLEXPRESS02;Initial Catalog=CourseWork;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AmazonCategory>(entity =>
        {
            entity.ToTable("amazon_categories");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<AmazonProduct>(entity =>
        {
            entity.HasKey(e => e.Asin);

            entity.ToTable("amazon_products");

            entity.Property(e => e.Asin)
                .HasMaxLength(50)
                .HasColumnName("asin");
            entity.Property(e => e.BoughtInLastMonth).HasColumnName("boughtInLastMonth");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(100)
                .HasColumnName("imgUrl");
            entity.Property(e => e.IsBestSeller).HasColumnName("isBestSeller");
            entity.Property(e => e.ListPrice)
                .HasColumnType("decimal(18, 10)")
                .HasColumnName("listPrice");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 10)")
                .HasColumnName("price");
            entity.Property(e => e.ProductUrl)
                .HasMaxLength(50)
                .HasColumnName("productURL");
            entity.Property(e => e.Reviews).HasColumnName("reviews");
            entity.Property(e => e.Stars)
                .HasColumnType("decimal(18, 10)")
                .HasColumnName("stars");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Category).WithMany(p => p.AmazonProducts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_amazon_products_amazon_categories");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
