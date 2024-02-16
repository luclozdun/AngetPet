using AngetPet.Domain.Models;
using AngetPet.Domain.Repositories;

namespace AngetPet.Infraestructure.Repositories
{
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        private readonly AngetpetDbContext context;

        public GenderRepository(AngetpetDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
