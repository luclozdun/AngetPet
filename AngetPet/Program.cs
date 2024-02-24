using AngetPet.Application.Implementation;
using AngetPet.Application.Implementations;
using AngetPet.Application.Service;
using AngetPet.Application.Services;
using AngetPet.Domain.Repositories;
using AngetPet.Domain.Repository;
using AngetPet.Infraestructure;
using AngetPet.Infraestructure.Authenticate;
using AngetPet.Infraestructure.Repositories;
using AngetPet.Infraestructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

var jwtConfig = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = jwtConfig.Audience,
            ValidIssuer = jwtConfig.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.SecretKey))
        };
    });

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IGenderRepository, GenderRepository>();
builder.Services.AddScoped<IPersonGenderRepository, PersonGenderRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IGenderService, GenderService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPersonGenderService, PersonGenderService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<ISkillService, SkillService>();

builder.Services.AddSingleton<JwtOptions>();

builder.Services.AddDbContext<AngetpetDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AngetPet",
        Description = "Backend para un sistema de Adopcion de mascotas.",
        Contact = new OpenApiContact
        {
            Name = "Luciano L."
        }
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
});
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        x.RoutePrefix = string.Empty;
    });
}

app.Run();
