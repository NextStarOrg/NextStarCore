using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace BlogDbContext;

public partial class NextStarBlogContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();
#endif
    }
}