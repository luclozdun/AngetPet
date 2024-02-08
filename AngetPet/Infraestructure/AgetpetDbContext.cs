using Microsoft.EntityFrameworkCore;

namespace AngetPet.Infraestructure
{
    public class AgetpetDbContext : DbContext
    {
        public AgetpetDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
