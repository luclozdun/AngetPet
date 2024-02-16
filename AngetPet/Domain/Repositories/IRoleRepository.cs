using AngetPet.Domain.Model;

namespace AngetPet.Domain.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> FindByName(string name);
    }
}
