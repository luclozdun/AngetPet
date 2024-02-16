using AngetPet.Domain.Models;

namespace AngetPet.Application.Dtos
{
    public class SkillRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public Skill ConvertToEntity()
        {
            return new Skill
            {
                Name = Name,
                Description = Description,
                Icon = Icon
            };
        }
    }

    public class SkillResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public SkillResponse(Skill skill)
        {
            Id = skill.Id;
            Name = skill.Name;
            Description = skill.Description;
            Icon = skill.Icon;
        }
    }
}
