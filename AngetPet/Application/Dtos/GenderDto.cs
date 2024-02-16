using AngetPet.Domain.Models;

namespace AngetPet.Application.Dtos
{
    public class GenderRequest
    {
        public string Name { get; set; }

        public Gender ConvertToEntity()
        {
               return new Gender { Name = Name };
        }
    }

    public class GenderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GenderResponse(Gender gender) 
        {
            Id = gender.Id;
            Name = gender.Name;
        }
    }
}
