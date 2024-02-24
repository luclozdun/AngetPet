using AngetPet.Domain.Model;
using AngetPet.Domain.Models;
using AngetPet.Infraestructure.Seeders;
using Microsoft.EntityFrameworkCore;

namespace AngetPet.Infraestructure
{
    public class AngetpetDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<PersonGender> PersonGenders { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetSkill> PetsSkill { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public AngetpetDbContext(DbContextOptions<AngetpetDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Animal>(x =>
            {
                x.ToTable("Animals");
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).HasColumnName("AnimalId").ValueGeneratedOnAdd();
                x.Property(x => x.Image).HasColumnName("Image");
                x.Property(x => x.Name).HasColumnName("Name");
            });

            builder.Entity<Gender>(x =>
            {
                x.ToTable("Genders");
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).HasColumnName("GenderId").ValueGeneratedOnAdd();
                x.Property(x => x.Name).HasColumnName("Name");
            });

            builder.Entity<PersonGender>(x =>
            {
                x.ToTable("PersonGenders");
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).HasColumnName("PersonGenderId").ValueGeneratedOnAdd();
                x.Property(x => x.Name).HasColumnName("Name");
            });

            builder.Entity<Role>(x =>
            {
                x.ToTable("Roles");
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).HasColumnName("RoleId").ValueGeneratedOnAdd();
                x.Property(x => x.Name).HasColumnName("Name");
            });

            builder.Entity<Size>(x =>
            {
                x.ToTable("Sizes");
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).HasColumnName("SizeId").ValueGeneratedOnAdd();
                x.Property(x => x.Name).HasColumnName("Name");
                x.Property(x => x.Name).HasColumnName("Description");
            });

            builder.Entity<Skill>(x =>
            {
                x.ToTable("Skills");
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).HasColumnName("SkillId").ValueGeneratedOnAdd();
                x.Property(x => x.Name).HasColumnName("Name");
                x.Property(x => x.Name).HasColumnName("Description");
                x.Property(x => x.Icon).HasColumnName("Icon");
            });

            builder.Entity<User>(x =>
            {
                x.ToTable("Users");
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).HasColumnName("UserId").ValueGeneratedOnAdd();
                x.Property(x => x.Name).HasColumnName("Name");
                x.Property(x => x.Email).HasColumnName("Email");
                x.Property(x => x.Password).HasColumnName("Password");
                x.Property(x => x.Image).HasColumnName("Image");
                x.Property(x => x.CodeRecover).HasColumnName("CodeRecover");
                x.Property(x => x.Created).HasColumnName("Created");
                x.Property(x => x.Updated).HasColumnName("Updated");
                x.Property(x => x.ExpiredCodeRecover).HasColumnName("ExpiredCodeRecover");
                x.Property(x => x.PersonGenderId).HasColumnName("PersonGenderId");
                x.Property(x => x.CodeVerify).HasColumnName("CodeVerify");

                x.Property(x => x.DateVerify).HasColumnName("DateVerify");
                x.Property(x => x.ExpiredVerifyCode).HasColumnName("ExpiredVerifyCode");
                x.HasOne(x => x.PersonGender)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.PersonGenderId);
            });

            builder.Entity<Pet>(x =>
            {
                x.ToTable("Pets");
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).HasColumnName("PetId").ValueGeneratedOnAdd();
                x.Property(x => x.Name).HasColumnName("Name");
                x.Property(x => x.SubDescription).HasColumnName("SubDescription");
                x.Property(x => x.IsAntiparasiticVaccine).HasColumnName("IsAntiparasiticVaccine");
                x.Property(x => x.DateAntiparasiticVaccine).HasColumnName("DateAntiparasiticVaccine");
                x.Property(x => x.Birthdate).HasColumnName("Birthdate");
                x.Property(x => x.Description).HasColumnName("Description");
                x.Property(x => x.Characteristic).HasColumnName("Characteristic");
                x.Property(x => x.Image).HasColumnName("Image");
                x.Property(x => x.AnimalId).HasColumnName("AnimalId");
                x.HasOne(x => x.Animal)
                .WithMany(x => x.Pets)
                .HasForeignKey(x => x.AnimalId);
                x.Property(x => x.GenderId).HasColumnName("GenderId");
                x.HasOne(x => x.Gender)
                .WithMany(x => x.Pets)
                .HasForeignKey(x => x.GenderId);
                x.Property(x => x.SizeId).HasColumnName("SizeId");
                x.HasOne(x => x.Size)
                .WithMany(x => x.Pets)
                .HasForeignKey(x => x.SizeId);
                x.Property(x => x.IsSterilized).HasColumnName("IsSterilized");
                x.Property(x => x.IsTrained).HasColumnName("IsTrained");
                x.Property(x => x.TextColor).HasColumnName("TextColor");
                x.Property(x => x.BackgroundColor).HasColumnName("BackgroundColor");
                x.Property(x => x.BackgroundColorIcon).HasColumnName("BackgroundColorIcon");
                x.Property(x => x.Created).HasColumnName("Created");
                x.Property(x => x.Updated).HasColumnName("Updated");
            });

            builder.Entity<UserRole>(x =>
            {
                x.ToTable("UserRoles");
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).HasColumnName("UserRoleId").ValueGeneratedOnAdd();
                x.Property(x => x.UserId).HasColumnName("UserId").IsRequired();
                x.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);
                x.Property(x => x.RoleId).HasColumnName("RoleId").IsRequired();
                x.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);
            });

            AngetpetSeeder.OnModelSeedData(builder);
        }
    }
}
