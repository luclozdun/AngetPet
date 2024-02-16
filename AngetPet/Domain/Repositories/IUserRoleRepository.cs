using AngetPet.Domain.Models;

namespace AngetPet.Domain.Repositories
{
    public interface IUserRoleRepository
    {
        Task<List<UserRole>> FindAll();
        Task<List<UserRole>> FindAllByUserId(int userId);
        void Add(UserRole userRole);
        void Remove(UserRole userRole);
    }
}
