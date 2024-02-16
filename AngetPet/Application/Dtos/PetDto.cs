using AngetPet.Application.Dto;
using AngetPet.Domain.Models;

namespace AngetPet.Application.Dtos
{
    public class PetRequest
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
        public int GenderId { get; set; }
        public int SizeId { get; set; }
        public bool IsSterilized { get; set; }
        public bool IsTrained { get; set; }
        public string TextColor { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundColorIcon { get; set; }

        public Pet ConvertToEntity()
        {
            return new Pet
            {
                Name = Name,
                SubDescription = SubDescription,
                IsAntiparasiticVaccine = IsAntiparasiticVaccine,
                DateAntiparasiticVaccine = DateAntiparasiticVaccine,
                Birthdate = Birthdate,
                Description = Description,
                Characteristic = Characteristic,
                Image = Image,
                AnimalId = AnimalId,
                GenderId = GenderId,
                SizeId = SizeId,
                IsSterilized = IsSterilized,
                IsTrained = IsTrained,
                TextColor = TextColor,
                BackgroundColor = BackgroundColor,
                BackgroundColorIcon = BackgroundColorIcon
            };
        }

    }

    public class PetResponse
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string SubDescription { get; set; }
        public bool IsAntiparasiticVaccine { get; set; }
        public DateTime? DateAntiparasiticVaccine { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Description { get; set; }
        public string Characteristic { get; set; }
        public string Image { get; set; }
        public int AnimalId { get; set; }
        public AnimalResponse Animal { get; set; }
        public int GenderId { get; set; }
        public GenderResponse Gender { get; set; }
        public int SizeId { get; set; }
        public SizeResponse Size { get; set; }
        public bool IsSterilized { get; set; }
        public bool IsTrained { get; set; }
        public string TextColor { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundColorIcon { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public PetResponse(Pet entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            SubDescription = entity.SubDescription;
            IsAntiparasiticVaccine = entity.IsAntiparasiticVaccine;
            DateAntiparasiticVaccine = entity.DateAntiparasiticVaccine;
            Birthdate = entity.Birthdate;
            Description = entity.Description;
            Characteristic = entity.Characteristic;
            Image = entity.Image;
            AnimalId = entity.AnimalId;
            Animal = new AnimalResponse(entity.Animal);
            GenderId = entity.GenderId;
            Gender = new GenderResponse(entity.Gender);
            SizeId = entity.SizeId;
            Size = new SizeResponse(entity.Size);
            IsSterilized = entity.IsSterilized;
            IsTrained = entity.IsTrained;
            TextColor = entity.TextColor;
            BackgroundColor = entity.BackgroundColor;
            BackgroundColorIcon = entity.BackgroundColorIcon;
            Created = entity.Created;
            Updated = entity.Updated;
        }
    }
}