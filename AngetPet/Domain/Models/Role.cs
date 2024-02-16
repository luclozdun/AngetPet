using AngetPet.Domain.Models;

namespace AngetPet.Domain.Model
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; } = new();

    }
}
