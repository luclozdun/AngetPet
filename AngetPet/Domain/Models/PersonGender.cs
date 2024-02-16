namespace AngetPet.Domain.Models
{
    public class PersonGender : Entity
    {
        public string Name { get; set; }
        public List<User> Users { get; } = new();
    }
}
