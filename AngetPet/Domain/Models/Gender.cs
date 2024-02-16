namespace AngetPet.Domain.Models
{
    public class Gender : Entity
    {
        public string Name { get; set; }
        public List<Pet> Pets { get; } = new();
    }
}
