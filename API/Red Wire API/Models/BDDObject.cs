using Microsoft.EntityFrameworkCore;

namespace Red_Wire_API.Models
{
    public interface BDDObject
    {
        public void PostChild(DbContext dbContext);
        public void Post(DbContext dbContext);
    }
}
