using AngetPet.Application.Dto;
using AngetPet.Domain.Models;

namespace AngetPet.Application.Service
{
    public interface IAnimalService : IService<Animal, AnimalRequest, AnimalResponse>
    {
    }
}
