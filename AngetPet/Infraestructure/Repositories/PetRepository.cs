using AngetPet.Application.Dtos;
using AngetPet.Application.Service;
using AngetPet.Domain.Models;
using AngetPet.Domain.Repositories;

namespace AngetPet.Infraestructure.Repositories
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        private readonly AngetpetDbContext context;

        public PetRepository(AngetpetDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
