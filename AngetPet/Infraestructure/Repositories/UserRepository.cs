using AngetPet.Domain.Models;
using AngetPet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AngetPet.Infraestructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AngetpetDbContext context;

        public UserRepository(AngetpetDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User> FindByEmail(string email)
        {
            return await context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
