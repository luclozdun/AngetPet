namespace AngetPet.Domain.Models
{
    public class Skill : Entity
    {   
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public List<PetSkill> PetSkills { get; } = new();
    }
}
