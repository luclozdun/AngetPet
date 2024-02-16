using AngetPet.Domain.Models;

namespace AngetPet.Application.Dtos
{
    public class SizeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Size ConvertToEntity()
        {
            return new Size
            {
                Name = Name,
                Description = Description
            };
        }
    }

    public class SizeResponse
    {
        public int Id { get;set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public SizeResponse(Size size)
        {
            Id = size.Id;
            Name = size.Name;
            Description = size.Description;
        }
    }
}
