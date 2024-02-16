namespace AngetPet.Domain.Models
{
    public class Size : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Pet> Pets { get; } = new();
    }
}
