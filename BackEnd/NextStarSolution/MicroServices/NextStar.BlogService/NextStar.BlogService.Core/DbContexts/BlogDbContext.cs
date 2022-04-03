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
        public virtual DbSet<ArticleContent> ArticleContents { get; set; } = null!;
        public virtual DbSet<ArticleLastContent> ArticleLastContents { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.HasIndex(e => e.Id, "Article_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.UpdatedTime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.HasKey(e => e.ArticleId)
                    .HasName("ArticleCategory_pk");

                entity.ToTable("ArticleCategory");

                entity.HasIndex(e => e.ArticleId, "ArticleCategory_ArticleId_uindex")
                    .IsUnique();

                entity.Property(e => e.ArticleId).ValueGeneratedNever();

                entity.HasOne(d => d.Article)
                    .WithOne(p => p.ArticleCategory)
                    .HasForeignKey<ArticleCategory>(d => d.ArticleId)
                    .HasConstraintName("ArticleCategory_Article_Id_fk");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ArticleCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ArticleCategory_Category_Id_fk");
            });

            modelBuilder.Entity<ArticleContent>(entity =>
            {
                entity.ToTable("ArticleContent");

                entity.HasIndex(e => e.Id, "ArticleContent_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.CommitMessage).HasMaxLength(200);

                entity.Property(e => e.CreatedTime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleContents)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("ArticleContent_Article_Id_fk");
            });

            modelBuilder.Entity<ArticleLastContent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ArticleLastContent");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.Id, "Category_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Category_Name_uindex")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdatedTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Url).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
