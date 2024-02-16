using AngetPet.Domain.Models;
using AngetPet.Domain.Repositories;

namespace AngetPet.Infraestructure.Repositories
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        private readonly AngetpetDbContext context;

        public AnimalRepository(AngetpetDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
