using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class EquipmentContext : DbContext
    {
        public EquipmentContext(DbContextOptions<EquipmentContext> options)
            : base(options)
        {
        }

        public DbSet<Equipment> Equipment { get; set; }
    }
}