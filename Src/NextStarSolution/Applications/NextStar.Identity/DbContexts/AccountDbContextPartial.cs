using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NextStar.Identity.AccountDbModels;

namespace NextStar.Identity.DbContexts;

public partial class AccountDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();
#endif
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(p=>p.Id)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });
        
        modelBuilder.Entity<UserLoginHistory>(entity =>
        {
            entity.Property(p=>p.Id)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });
        
        modelBuilder.Entity<UserThirdPartyLogin>(entity =>
        {
            entity.Property(p=>p.Id)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });
    }
}