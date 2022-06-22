using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class RedWireContext : DbContext
    {
        public RedWireContext(DbContextOptions<RedWireContext> options)
            : base(options)
        {
        }

        public DbSet<RedWire> RedWire { get; set; }
    }
}