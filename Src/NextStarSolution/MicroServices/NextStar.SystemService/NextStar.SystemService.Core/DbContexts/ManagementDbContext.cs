using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NextStar.SystemService.Core.ManagementDbModels;

namespace NextStar.SystemService.Core.DbContexts
{
    public partial class ManagementDbContext : DbContext
    {
        public ManagementDbContext()
        {
        }

        public ManagementDbContext(DbContextOptions<ManagementDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationConfig> ApplicationConfigs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationConfig>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("ApplicationConfig_pk");

                entity.ToTable("ApplicationConfig");

                entity.HasIndex(e => e.Id, "ApplicationConfig_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "ApplicationConfig_Key_uindex")
                    .IsUnique();

                entity.Property(e => e.Key).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Environment).HasMaxLength(20);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Value).HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
