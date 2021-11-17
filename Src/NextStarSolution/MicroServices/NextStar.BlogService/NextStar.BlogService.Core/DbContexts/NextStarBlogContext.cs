using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NextStar.BlogService.Core.NextStarBlogDbModels;

namespace BlogDbContext
{
    public partial class NextStarBlogContext : DbContext
    {
        public NextStarBlogContext()
        {
        }

        public NextStarBlogContext(DbContextOptions<NextStarBlogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<ArticleAuthor> ArticleAuthors { get; set; } = null!;
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; } = null!;
        public virtual DbSet<ArticleContent> ArticleContents { get; set; } = null!;
        public virtual DbSet<ArticlePartial> ArticlePartials { get; set; } = null!;
        public virtual DbSet<ArticleTag> ArticleTags { get; set; } = null!;
        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<AuthorProfile> AuthorProfiles { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("Article_pk")
                    .IsClustered(false);

                entity.ToTable("Article");

                entity.HasIndex(e => e.Id, "Article_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "Article_Key_uindex")
                    .IsUnique();

                entity.Property(e => e.Key).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UpdatedTime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ArticleAuthor>(entity =>
            {
                entity.HasKey(e => e.ArticleKey)
                    .HasName("ArticleAuthor_pk")
                    .IsClustered(false);

                entity.ToTable("ArticleAuthor");

                entity.HasIndex(e => e.ArticleKey, "ArticleAuthor_ArticleKey_uindex")
                    .IsUnique();

                entity.Property(e => e.ArticleKey).ValueGeneratedNever();

                entity.HasOne(d => d.ArticleKeyNavigation)
                    .WithOne(p => p.ArticleAuthor)
                    .HasForeignKey<ArticleAuthor>(d => d.ArticleKey)
                    .HasConstraintName("ArticleAuthor_Article_Key_fk");

                entity.HasOne(d => d.AuthorKeyNavigation)
                    .WithMany(p => p.ArticleAuthors)
                    .HasForeignKey(d => d.AuthorKey)
                    .HasConstraintName("ArticleAuthor_Author_Key_fk");
            });

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ArticleCategory");

                entity.HasOne(d => d.ArticleKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ArticleKey)
                    .HasConstraintName("ArticleCategory_Article_Key_fk");

                entity.HasOne(d => d.CategoryKeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryKey)
                    .HasConstraintName("ArticleCategory_Category_Key_fk");
            });

            modelBuilder.Entity<ArticleContent>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("ArticleContent_pk")
                    .IsClustered(false);

                entity.ToTable("ArticleContent");

                entity.HasIndex(e => e.Id, "ArticleContent_Id_uindex")
                    .IsUnique();

                entity.Property(e => e.CreatedTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Origin).HasComment("markdown text");

                entity.HasOne(d => d.ArticleKeyNavigation)
                    .WithMany(p => p.ArticleContents)
                    .HasForeignKey(d => d.ArticleKey)
                    .HasConstraintName("ArticleContent_Article_Key_fk");
            });

            modelBuilder.Entity<ArticlePartial>(entity =>
            {
                entity.HasKey(e => e.ArticleKey)
                    .HasName("ArticlePartial_pk")
                    .IsClustered(false);

                entity.ToTable("ArticlePartial");

                entity.HasIndex(e => e.AliasUrl, "ArticlePartial_AliasUrl_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.ArticleKey, "ArticlePartial_ArticleKey_uindex")
                    .IsUnique();

                entity.Property(e => e.ArticleKey).ValueGeneratedNever();

                entity.Property(e => e.AliasUrl).HasMaxLength(100);

                entity.Property(e => e.UpdatedTime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ArticleKeyNavigation)
                    .WithOne(p => p.ArticlePartial)
                    .HasForeignKey<ArticlePartial>(d => d.ArticleKey)
                    .HasConstraintName("ArticlePartial_Article_Key_fk");
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

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("Author_pk")
                    .IsClustered(false);

                entity.ToTable("Author");

                entity.HasIndex(e => e.Id, "Author_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "Author_Key_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Author_Name_uindex")
                    .IsUnique();

                entity.Property(e => e.Key).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<AuthorProfile>(entity =>
            {
                entity.HasKey(e => e.AuthorKey)
                    .HasName("AuthorProfile_pk")
                    .IsClustered(false);

                entity.ToTable("AuthorProfile");

                entity.HasIndex(e => e.AuthorKey, "AuthorProfile_AuthorKey_uindex")
                    .IsUnique();

                entity.Property(e => e.AuthorKey).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Url).HasMaxLength(512);

                entity.HasOne(d => d.AuthorKeyNavigation)
                    .WithOne(p => p.AuthorProfile)
                    .HasForeignKey<AuthorProfile>(d => d.AuthorKey)
                    .HasConstraintName("AuthorProfile_Author_Key_fk");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("Category_pk")
                    .IsClustered(false);

                entity.ToTable("Category");

                entity.HasIndex(e => e.Id, "Category_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "Category_Key_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Category_Name_uindex")
                    .IsUnique();

                entity.Property(e => e.Key).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdatedTime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("Tag_pk")
                    .IsClustered(false);

                entity.ToTable("Tag");

                entity.HasIndex(e => e.Id, "Tag_Id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "Tag_Key_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Tag_Name_uindex")
                    .IsUnique();

                entity.Property(e => e.Key).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdatedTime).HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
