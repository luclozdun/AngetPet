using AngetPet.Domain.Model;
using AngetPet.Domain.Models;
using AngetPet.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AngetPet.Infraestructure.Seeders
{
    public class AngetpetSeeder
    {
        public static void OnModelSeedData(ModelBuilder builder)
        {
            SeederRole(builder);
            SeederGender(builder);
            SeederPersonGender(builder);
        }

        private static void SeederRole(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role { Id = 1, Name = ConstantHelper.Role.ADMIN },
                new Role { Id = 2, Name = ConstantHelper.Role.CUSTOMER }
                );
        }

        private static void SeederGender(ModelBuilder builder)
        {
            builder.Entity<Gender>().HasData(
                new Gender { Id = 1, Name = "Macho" },
                new Gender { Id = 2, Name = "Hembra" },
                new Gender { Id = 3, Name = "Sin Especificar" }
                );
        }

        private static void SeederPersonGender(ModelBuilder builder)
        {
            builder.Entity<PersonGender>().HasData(
                new PersonGender { Id = 1, Name = "Masculino" },
                new PersonGender { Id = 2, Name = "Femenino" },
                new PersonGender { Id = 3, Name = "Prefiero no decir" }
                );
        }
    }
}
