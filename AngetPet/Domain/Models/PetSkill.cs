namespace AngetPet.Domain.Models
{
    public class PetSkill : Entity
    {
        public int PetId { get; set; }
        public int SkillId { get; set; }
        public int Score { get; set; }
        public Pet Pet { get; set; }
        public Skill Skill { get; set; }
    }
}
