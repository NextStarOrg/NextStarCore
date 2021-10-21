using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NextStar.Framework.AspNetCore.NextStarSessionDbModels;

#nullable disable

namespace NextStar.Framework.AspNetCore.DbContexts
{
    public partial class NextStarSessionDbContext : DbContext
    {
        public virtual DbSet<UserSession> UserSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("UserSession_pk")
                    .IsClustered(false);

                entity.ToTable("UserSession");

                entity.HasIndex(e => e.Id, "UserSession_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
