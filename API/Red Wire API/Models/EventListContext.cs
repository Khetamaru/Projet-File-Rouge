using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class EventListContext : DbContext
    {
        public EventListContext(DbContextOptions<EventListContext> options)
            : base(options)
        {
        }

        public DbSet<EventList> EventList { get; set; }
    }
}