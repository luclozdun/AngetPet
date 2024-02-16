using AngetPet.Domain.Models;
using AngetPet.Domain.Repositories;

namespace AngetPet.Infraestructure.Repositories
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        private readonly AngetpetDbContext context;

        public SkillRepository(AngetpetDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
