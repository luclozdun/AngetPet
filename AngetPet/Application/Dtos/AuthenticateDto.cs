using AngetPet.Domain.Models;

namespace AngetPet.Application.Dtos
{
    public class SignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignUpRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Image { get; set; }
        public int PersonGenderId { get; set; }

        public User ConvertToEntity()
        {
            return new User
            {
                Name = Name,
                Email = Email,
                Password = Password,
                Image = Image,
                PersonGenderId = PersonGenderId
            };
        }

        public class AuthenticateToken
        {
            public string Token { get; set; }
            public AuthenticateToken(string token)
            {
                Token = token;
            }


        }
    }
}
