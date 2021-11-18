using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace NextStar.BlogService.Core.DbContexts;

public partial class BlogDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();
#endif
    }
}