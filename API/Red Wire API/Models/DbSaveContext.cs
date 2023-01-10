using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class DbSaveContext : DbContext
    {
        public DbSaveContext(DbContextOptions<DbSaveContext> options)
            : base(options)
        {
        }

        public DbSet<DbSave> DbSave { get; set; }
    }
}