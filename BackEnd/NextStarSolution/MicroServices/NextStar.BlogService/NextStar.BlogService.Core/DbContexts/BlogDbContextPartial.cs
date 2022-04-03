using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NextStar.BlogService.Core.BlogDbModels;

namespace NextStar.BlogService.Core.DbContexts;

public partial class BlogDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();
#endif
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        //empty
    }
}