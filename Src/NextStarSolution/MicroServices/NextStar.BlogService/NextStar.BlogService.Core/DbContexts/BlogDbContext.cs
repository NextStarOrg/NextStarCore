using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NextStar.BlogService.Core.BlogDbModels;

namespace NextStar.BlogService.Core.DbContexts
{
    public partial class BlogDbContext : DbContext
    {
        public BlogDbContext()
        {
        }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; } = null!;
        public virtual DbSet<ArticleCodeEnvironment> ArticleCodeEnvironments { get; set; } = null!;
        public virtual DbSet<ArticleContent> ArticleContents { get; set; } = null!;
        public virtual DbSet<ArticleLastContent> ArticleLastContents { get; set; } = null!;
        public virtual DbSet<ArticleTag> ArticleTags { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CodeEnvironment> CodeEnvironments { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("Article_pk");

                entity.ToTable("Article");

                entity.HasIndex(e => e.Id, "Article_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "Article_Key_uindex")
                    .IsUnique();

                entity.Property(e => e.Key).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Title).HasMaxLength(300);
            });

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ArticleCategory");

                entity.HasIndex(e => e.ArticleKey, "ArticleCategory_ArticleKey_uindex")
                    .IsUnique();

                entity.HasOne(d => d.ArticleKeyNavigation)
                    .WithOne()
                    .HasForeignKey<ArticleCategory>(d => d.ArticleKey)
                    .HasConstraintName("ArticleCategory_Article_Key_fk");

                entity.HasOne(d => d.CategoryKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryKey)
                    .HasConstraintName("ArticleCategory_Category_Key_fk");
            });

            modelBuilder.Entity<ArticleCodeEnvironment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ArticleCodeEnvironment");

                entity.HasOne(d => d.ArticleKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ArticleKey)
                    .HasConstraintName("ArticleEnvironment_Article_Key_fk");

                entity.HasOne(d => d.EnvironmentKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.EnvironmentKey)
                    .HasConstraintName("ArticleEnvironment_Environment_Key_fk");
            });

            modelBuilder.Entity<ArticleContent>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ArticleContent");

                entity.HasIndex(e => e.CreatedTime, "Article_CreatedTime_index");

                entity.HasOne(d => d.ArticleKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ArticleKey)
                    .HasConstraintName("Article_Article_Key_fk");
            });

            modelBuilder.Entity<ArticleLastContent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ArticleLastContent");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Title).HasMaxLength(300);
            });

            modelBuilder.Entity<ArticleTag>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ArticleTag");

                entity.HasOne(d => d.ArticleKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ArticleKey)
                    .HasConstraintName("ArticleTag_Article_Key_fk");

                entity.HasOne(d => d.TagKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.TagKey)
                    .HasConstraintName("ArticleTag_Tag_Key_fk");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("Category_pk");

                entity.ToTable("Category");

                entity.HasIndex(e => e.Id, "Category_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "Category_Key_uindex")
                    .IsUnique();

                entity.Property(e => e.Key).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<CodeEnvironment>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("Environment_pk");

                entity.ToTable("CodeEnvironment");

                entity.HasIndex(e => e.Id, "Environment_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "Environment_Key_uindex")
                    .IsUnique();

                entity.Property(e => e.Key).ValueGeneratedNever();

                entity.Property(e => e.IconUrl).HasMaxLength(200);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("Tag_pk");

                entity.ToTable("Tag");

                entity.HasIndex(e => e.Id, "Tag_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "Tag_Key_uindex")
                    .IsUnique();

                entity.Property(e => e.Key).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.BackgroundColor)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("('#000000')");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.TextColor)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("('#FFFFFF')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
