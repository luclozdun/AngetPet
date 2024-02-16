namespace AngetPet.Domain.Models
{
    public class Pet : Entity
    {
        public string Name { get; set; }
        public string SubDescription { get; set; }
        public bool IsAntiparasiticVaccine { get; set; }
        public DateTime? DateAntiparasiticVaccine { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Description { get; set; }
        public string Characteristic { get; set; }
        public string Image { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
        public bool IsSterilized { get; set; }
        public bool IsTrained { get; set; }
        public string TextColor { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundColorIcon { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public List<PetSkill> PetSkills { get; } = new();
    }
}
