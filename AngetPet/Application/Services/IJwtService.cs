using AngetPet.Domain.Models;

namespace AngetPet.Application.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user, string role);
    }
}
