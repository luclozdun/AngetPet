namespace AngetPet.Domain.Models
{
    public class User : Entity
    {
        public string? Name { get; set; }
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
        public PersonGender PersonGender { get; set; }
        public List<UserRole> UserRoles { get; set; } = new();

    }
}
