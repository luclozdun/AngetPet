using AngetPet.Domain.Models;
using AngetPet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AngetPet.Infraestructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AngetpetDbContext context;

        public UserRoleRepository(AngetpetDbContext context)
        {
            this.context = context;
        }

        public void Add(UserRole userRole)
        {
            context.UserRoles.Add(userRole);
        }

        public async Task<List<UserRole>> FindAll()
        {
            return await context.UserRoles.ToListAsync();
        }

        public async Task<List<UserRole>> FindAllByUserId(int userId)
        {
            return await context.UserRoles.Where(x => x.UserId == userId).Include(x => x.Role).ToListAsync();
        }

        public void Remove(UserRole userRole)
        {
            context.UserRoles.Remove(userRole);
        }
    }
}
