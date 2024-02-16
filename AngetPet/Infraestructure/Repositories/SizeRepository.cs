using AngetPet.Domain.Models;
using AngetPet.Domain.Repositories;

namespace AngetPet.Infraestructure.Repositories
{
    public class SizeRepository : Repository<Size>, ISizeRepository
    {
        private readonly AngetpetDbContext context;

        public SizeRepository(AngetpetDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
