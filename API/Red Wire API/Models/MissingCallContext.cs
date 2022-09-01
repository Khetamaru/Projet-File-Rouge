using Microsoft.EntityFrameworkCore;

namespace Projet_File_Rouge.Object
{
    public class MissingCallContext : DbContext
    {
        public MissingCallContext(DbContextOptions<MissingCallContext> options)
            : base(options)
        {
        }

        public DbSet<MissingCall> MissingCall { get; set; }
    }
}
