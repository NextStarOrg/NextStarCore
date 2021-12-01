﻿using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace NextStar.Identity.DbContexts;

public partial class AccountDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();
#endif
    }
}