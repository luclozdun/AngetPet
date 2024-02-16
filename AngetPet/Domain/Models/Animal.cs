namespace AngetPet.Domain.Models
{
    public class Animal : Entity
    {
        public Animal(string name, string image, string icon)
        {
            Name = name;
            Image = image;
            Icon = icon;
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public List<Pet> Pets { get; } = new();
    }
}
