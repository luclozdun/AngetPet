using AngetPet.Application.Dtos;
using AngetPet.Application.Service;
using AngetPet.Domain.Models;

namespace AngetPet.Application.Services
{
    public interface ISkillService : IService<Skill, SkillRequest, SkillResponse>
    {
    }
}
