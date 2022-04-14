using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Event { get; set; }
    }
}