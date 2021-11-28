using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace NextStar.Library.AspNetCore.DbContexts;

public partial class ManagementDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();
#endif
    }
}