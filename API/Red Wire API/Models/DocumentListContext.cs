using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class DocumentListContext : DbContext
    {
        public DocumentListContext(DbContextOptions<DocumentListContext> options)
            : base(options)
        {
        }

        public DbSet<DocumentList> DocumentList { get; set; }
    }
}