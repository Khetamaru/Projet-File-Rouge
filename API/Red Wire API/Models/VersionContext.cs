using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class VersionContext : DbContext
    {
        public VersionContext(DbContextOptions<VersionContext> options)
            : base(options)
        {
        }

        public DbSet<Version> Version { get; set; }
    }
}