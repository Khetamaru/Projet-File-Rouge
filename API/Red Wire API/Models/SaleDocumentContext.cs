using Microsoft.EntityFrameworkCore;
using Projet_File_Rouge.Object;

namespace Local_API_Server.Models
{
    public class SaleDocumentContext : DbContext
    {
        public SaleDocumentContext(DbContextOptions<SaleDocumentContext> options)
            : base(options)
        {
        }

        public DbSet<SaleDocument> SaleDocument { get; set; }
    }
}