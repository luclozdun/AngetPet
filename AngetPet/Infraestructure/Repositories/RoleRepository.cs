using AngetPet.Domain.Model;
using AngetPet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AngetPet.Infraestructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly AngetpetDbContext context;

        public RoleRepository(AngetpetDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Role> FindByName(string name)
        {
            return await context.Roles.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
