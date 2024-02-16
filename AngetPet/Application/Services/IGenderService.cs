using AngetPet.Application.Dtos;
using AngetPet.Application.Service;
using AngetPet.Domain.Models;

namespace AngetPet.Application.Services
{
    public interface IGenderService : IService<Gender, GenderRequest, GenderResponse>
    {
    }
}
