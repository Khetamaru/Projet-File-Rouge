using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class EvenementContext : DbContext
    {
        public EvenementContext(DbContextOptions<EvenementContext> options)
            : base(options)
        {
        }

        public DbSet<Evenement> Evenement { get; set; }
    }
}