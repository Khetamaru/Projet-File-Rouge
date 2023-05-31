using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class CommandListContext : DbContext
    {
        public CommandListContext(DbContextOptions<CommandListContext> options)
            : base(options)
        {
        }

        public DbSet<CommandList> CommandList { get; set; }
    }
}