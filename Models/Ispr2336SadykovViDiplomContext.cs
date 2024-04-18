using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Models;

public partial class Ispr2336SadykovViDiplomContext : DbContext
{
    public Ispr2336SadykovViDiplomContext()
    {
    }

    public Ispr2336SadykovViDiplomContext(DbContextOptions<Ispr2336SadykovViDiplomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<DishesType> DishesTypes { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuDish> MenuDishes { get; set; }

    public virtual DbSet<MenuSchedule> MenuSchedules { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=cfif31.ru;database=ISPr23-36_SadykovVI_Diplom;uid=ISPr23-36_SadykovVI;pwd=ISPr23-36_SadykovVI", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.TeacherId, "FK_Classes_TeachersId_idx");

            entity.Property(e => e.ClassName).HasMaxLength(3);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Classes_TeachersId");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.DishesTypeId, "FK_Dishes_DishesTypeId_idx");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.DishesType).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.DishesTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dishes_DishesTypeId");
        });

        modelBuilder.Entity<DishesType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("DishesType");

            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<MenuDish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.DishId, "FK_MenuDishes_DishId_idx");

            entity.HasIndex(e => e.MenuId, "FK_MenuDishes_MenuId_idx");

            entity.HasOne(d => d.Dish).WithMany(p => p.MenuDishes)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuDishes_DishId");

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuDishes)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuDishes_MenuId");
        });

        modelBuilder.Entity<MenuSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("MenuSchedule");

            entity.HasIndex(e => e.MenuId, "FK_MenuSchedule_MenuId_idx");

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuSchedules)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuSchedule_MenuId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.DishId, "FK_Recipes_DishId_idx");

            entity.HasIndex(e => e.ProductId, "FK_Recipes_ProductId_idx");

            entity.Property(e => e.ProductQuantity).HasMaxLength(45);

            entity.HasOne(d => d.Dish).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recipes_DishId");

            entity.HasOne(d => d.Product).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recipes_ProductId");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.ClassId, "FK_Students_ClassId_idx");

            entity.HasIndex(e => e.StatusId, "FK_Students_StatusId_idx");

            entity.Property(e => e.FullName).HasMaxLength(100);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_ClassId");

            entity.HasOne(d => d.Status).WithMany(p => p.Students)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_StatusId");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.UserId, "FK_Teachers_UserId_idx");

            entity.Property(e => e.FullName).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teachers_UserId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.UserTypeId, "FK_Users_UserTypeId_idx");

            entity.Property(e => e.Login).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(45);

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_UserTypeId");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("UserType");

            entity.Property(e => e.Type).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
