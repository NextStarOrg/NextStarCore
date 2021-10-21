using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NextStar.IdentityServer.NextStarAccountDbModels;

#nullable disable

namespace NextStar.IdentityServer.DbContexts
{
    public partial class NextStarAccountDbContext : DbContext
    {
        public NextStarAccountDbContext()
        {
        }

        public NextStarAccountDbContext(DbContextOptions<NextStarAccountDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ThirdParty> ThirdParties { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserHistory> UserHistories { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ThirdParty>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("ThirdParty_pk")
                    .IsClustered(false);

                entity.ToTable("ThirdParty");

                entity.HasIndex(e => e.Id, "ThirdParty_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.ThirdParties)
                    .HasForeignKey(d => d.UserKey)
                    .HasConstraintName("ThirdParty_User_Key_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("User_pk")
                    .IsClustered(false);

                entity.ToTable("User");

                entity.HasIndex(e => e.Id, "User_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "User_Key_uindex")
                    .IsUnique();

                entity.Property(e => e.Key).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UserHistory>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("UserHistory_pk")
                    .IsClustered(false);

                entity.ToTable("UserHistory");

                entity.HasIndex(e => e.Id, "UserHistory_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.BrowserName).HasMaxLength(100);

                entity.Property(e => e.BrowserVersion).HasMaxLength(10);

                entity.Property(e => e.Fingerprint).HasMaxLength(100);

                entity.Property(e => e.Ip).HasMaxLength(20);

                entity.Property(e => e.Platform).HasMaxLength(50);

                entity.Property(e => e.SystemName).HasMaxLength(100);

                entity.Property(e => e.SystemVersion).HasMaxLength(10);

                entity.Property(e => e.UserAgent).HasMaxLength(200);

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithMany(p => p.UserHistories)
                    .HasForeignKey(d => d.UserKey)
                    .HasConstraintName("UserHistory_User_Key_fk");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserKey)
                    .HasName("UserProfile_pk")
                    .IsClustered(false);

                entity.ToTable("UserProfile");

                entity.HasIndex(e => e.UserKey, "UserProfile_UserKey_uindex")
                    .IsUnique();

                entity.Property(e => e.UserKey).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.NickName).HasMaxLength(100);

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(600);

                entity.Property(e => e.UpdatedTime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.UserKeyNavigation)
                    .WithOne(p => p.UserProfile)
                    .HasForeignKey<UserProfile>(d => d.UserKey)
                    .HasConstraintName("UserProfile_User_Key_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
