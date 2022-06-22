using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class SaleDocumentLineContext : DbContext
    {
        public SaleDocumentLineContext(DbContextOptions<SaleDocumentLineContext> options)
            : base(options)
        {
        }

        public DbSet<SaleDocumentLine> SaleDocumentLine { get; set; }
    }
}