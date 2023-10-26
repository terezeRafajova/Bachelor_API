using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bachelor_API.Model;

namespace Bachelor_API.Data
{
    public class Bachelor_APIContext : DbContext
    {
        public Bachelor_APIContext (DbContextOptions<Bachelor_APIContext> options)
            : base(options)
        {
        }

        public DbSet<Bachelor_API.Model.UnitPlan> UnitPlan { get; set; } = default!;
    }
}
