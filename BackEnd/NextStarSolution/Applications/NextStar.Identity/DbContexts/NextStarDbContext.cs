using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NextStar.Identity.NextStarDbModels;

namespace NextStar.Identity.DbContexts
{
    public partial class NextStarDbContext : DbContext
    {
        public NextStarDbContext()
        {
        }

        public NextStarDbContext(DbContextOptions<NextStarDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserLoginHistory> UserLoginHistories { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "User_Email_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "User_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.CreatedTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(100);
            });

            modelBuilder.Entity<UserLoginHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserLoginHistory");

                entity.Property(e => e.FingerprintId).HasMaxLength(50);

                entity.Property(e => e.IpV4).HasMaxLength(20);

                entity.Property(e => e.IpV6).HasMaxLength(50);

                entity.Property(e => e.LoginType)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('Normal')");

                entity.Property(e => e.OtherInfo).HasMaxLength(100);

                entity.Property(e => e.UserAgent).HasMaxLength(200);

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UserLoginHistory_User_Id_fk");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.LoginName)
                    .HasName("UserProfile_pk");

                entity.ToTable("UserProfile");

                entity.HasIndex(e => e.LoginName, "UserProfile_LoginName_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "UserProfile_UserId_uindex")
                    .IsUnique();

                entity.Property(e => e.LoginName).HasMaxLength(50);

                entity.Property(e => e.NickName).HasMaxLength(50);

                entity.Property(e => e.PassWord)
                    .HasMaxLength(100)
                    .HasComment("Base64(nextstar_{salt}_xA123456)");

                entity.Property(e => e.Salt).HasMaxLength(50);

                entity.Property(e => e.UpdatedTime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserProfile)
                    .HasForeignKey<UserProfile>(d => d.UserId)
                    .HasConstraintName("UserProfile_User_Id_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
