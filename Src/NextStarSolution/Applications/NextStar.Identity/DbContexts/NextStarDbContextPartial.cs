using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NextStar.Identity.NextStarDbModels;

namespace NextStar.Identity.DbContexts;

public partial class NextStarDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();
#endif
    }
}