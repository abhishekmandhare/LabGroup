using Microsoft.EntityFrameworkCore;
using TFNAPI.Infrastructure.Data.Entities;


namespace TFNAPI.Infrastructure.Data.Context
{
    public class TFNDBContext : DbContext
    {
        public TFNDBContext(DbContextOptions<TFNDBContext> options)
        : base(options) { }
        public DbSet<Attempt> Attempts { get; set; }

       public DbSet<WeightingFactors> WFactors { get; set; }
    }
}
