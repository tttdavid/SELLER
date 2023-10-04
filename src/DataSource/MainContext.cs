using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.DataSource
{
    public partial class MainContext : DbContext
    {
        public MainContext()
        {
        }

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("connection"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Products__3213E83FB113990D");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AuthorId).HasColumnName("authorId");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F37F99BE8");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Avatar).HasColumnName("avatar");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");
                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
