using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NextStar.Framework.AspNetCore.DbContexts
{
    public partial class NextStarSessionDbContext : DbContext
    {
        public NextStarSessionDbContext()
        {
        }

        public NextStarSessionDbContext(DbContextOptions<NextStarSessionDbContext> options)
            : base(options)
        {
        }
    }
}
