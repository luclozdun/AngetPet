using AngetPet.Domain.Models;

namespace AngetPet.Application.Dtos
{
    public class PersonGenderRequest
    {
        public string Name { get; set; }

        public PersonGender ConvertToEntity()
        {
            return new PersonGender { Name = Name };
        }
    }

    public class PersonGenderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PersonGenderResponse(PersonGender entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
