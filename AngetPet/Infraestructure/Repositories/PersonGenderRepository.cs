using AngetPet.Domain.Models;
using AngetPet.Domain.Repositories;

namespace AngetPet.Infraestructure.Repositories
{
    public class PersonGenderRepository : Repository<PersonGender>, IPersonGenderRepository
    {
        private readonly AngetpetDbContext context;
        public PersonGenderRepository(AngetpetDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
