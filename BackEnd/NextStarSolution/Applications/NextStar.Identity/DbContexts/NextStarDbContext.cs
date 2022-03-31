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
        public virtual DbSet<UserThirdPartyLogin> UserThirdPartyLogins { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("User_pk");

                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "User_Email_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "User_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "User_Key_uindex")
                    .IsUnique();

                entity.Property(e => e.Key).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UserLoginHistory>(entity =>
            {
                entity.ToTable("UserLoginHistory");

                entity.HasIndex(e => e.Id, "UserLoginHistory_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.IpV4).HasMaxLength(20);

                entity.Property(e => e.IpV6).HasMaxLength(50);

                entity.Property(e => e.LoginType).HasMaxLength(50);

                entity.Property(e => e.UserAgent).HasMaxLength(200);

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.UserLoginHistories)
                    .HasForeignKey(d => d.UserKey)
                    .HasConstraintName("UserLoginHistory_User_Key_fk");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserKey)
                    .HasName("UserProfile_pk");

                entity.ToTable("UserProfile");

                entity.HasIndex(e => e.LoginName, "UserProfile_LoginName_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.UserKey, "UserProfile_UserKey_uindex")
                    .IsUnique();

                entity.Property(e => e.UserKey).ValueGeneratedNever();

                entity.Property(e => e.DisplayName).HasMaxLength(100);

                entity.Property(e => e.LoginName).HasMaxLength(50);

                entity.Property(e => e.PassWord).HasMaxLength(512);

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithOne(p => p.UserProfile)
                    .HasForeignKey<UserProfile>(d => d.UserKey)
                    .HasConstraintName("UserProfile_User_Key_fk");
            });

            modelBuilder.Entity<UserThirdPartyLogin>(entity =>
            {
                entity.ToTable("UserThirdPartyLogin");

                entity.HasIndex(e => e.Id, "UserThirdPartyLogin_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.LoginType).HasMaxLength(50);

                entity.Property(e => e.ThirdPartyEmail).HasMaxLength(100);

                entity.Property(e => e.ThirdPartyKey).HasMaxLength(200);

                entity.Property(e => e.ThirdPartyName).HasMaxLength(100);

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.UserThirdPartyLogins)
                    .HasForeignKey(d => d.UserKey)
                    .HasConstraintName("UserThirdPartyLogin_User_Key_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
