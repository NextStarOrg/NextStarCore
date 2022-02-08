using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NextStar.Library.AspNetCore.ApplicationDbModels;

namespace NextStar.Library.AspNetCore.DbContexts
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationConfig> ApplicationConfigs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationConfig>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("ApplicationConfig_pk");

                entity.ToTable("ApplicationConfig");

                entity.HasIndex(e => e.Id, "ApplicationConfig_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "ApplicationConfig_Name_uindex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Value).HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
