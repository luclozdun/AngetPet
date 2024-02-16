using AngetPet.Domain.Models;

namespace AngetPet.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByEmail(string email);
    }
}
