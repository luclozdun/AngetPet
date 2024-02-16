using AngetPet.Domain.Models;
using AngetPet.Shared.Helpers;

namespace AngetPet.Application.Dto
{
    public class AnimalRequest
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }

        public Animal ConvertToEntity()
        {
            return new Animal(Name, Image, Icon);
        }
    }

    public class AnimalResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }

        public AnimalResponse(Animal animal)
        {
            Id = animal.Id;
            Name = animal.Name;
            Image = animal.Image;
            Icon = animal.Icon;
        }

        public static AnimalResponse ConvertToEntity(Animal animal)
        {
            return new AnimalResponse(animal);
        }
    }
}
