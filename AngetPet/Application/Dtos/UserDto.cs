using AngetPet.Domain.Models;

namespace AngetPet.Application.Dtos
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Image { get; set; }
        public string? CodeRecover { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? ExpiredCodeRecover { get; set; }
        public string CodeVerify { get; set; }
        public DateTime? DateVerify { get; set; }
        public DateTime? ExpiredVerifyCode { get; set; }
        public int PersonGenderId { get; set; }
        public PersonGenderResponse PersonGender { get; set; }

        public UserResponse(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
            Image = user.Image;
            CodeRecover = user.CodeRecover;
            Created = user.Created;
            Updated = user.Updated;
            ExpiredCodeRecover = user.ExpiredCodeRecover;
            CodeVerify = user.CodeVerify;
            DateVerify = user.DateVerify;
            ExpiredVerifyCode = user.ExpiredVerifyCode;
            PersonGenderId = user.PersonGenderId;
            PersonGender = new PersonGenderResponse(user.PersonGender);
        }
    }
}
