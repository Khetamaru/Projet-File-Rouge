using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class DocumentFTPContext : DbContext
    {
        public DocumentFTPContext(DbContextOptions<DocumentFTPContext> options)
            : base(options)
        {
        }

        public DbSet<DocumentFTP> DocumentFTP { get; set; }
    }
}