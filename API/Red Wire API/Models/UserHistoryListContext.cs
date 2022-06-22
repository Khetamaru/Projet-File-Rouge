using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class UserHistoryListContext : DbContext
    {
        public UserHistoryListContext(DbContextOptions<UserHistoryListContext> options)
            : base(options)
        {
        }

        public DbSet<UserHistoryList> UserHistoryList { get; set; }
    }
}