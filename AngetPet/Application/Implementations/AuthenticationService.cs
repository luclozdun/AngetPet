using AngetPet.Application.Dtos;
using AngetPet.Application.Services;
using AngetPet.Domain.Models;
using AngetPet.Domain.Repositories;
using AngetPet.Domain.Repository;
using AngetPet.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using static AngetPet.Application.Dtos.SignUpRequest;

namespace AngetPet.Application.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IPersonGenderRepository personGenderRepository;
        private readonly IJwtService jwtService;
        private readonly IRoleRepository roleRepository;

        public AuthenticationService(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserRoleRepository userRoleRepository, IPersonGenderRepository personGenderRepository, IJwtService jwtService, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.userRoleRepository = userRoleRepository;
            this.personGenderRepository = personGenderRepository;
            this.jwtService = jwtService;
            this.roleRepository = roleRepository;
        }

        public async Task<ResultBase<AuthenticateToken>> SignIn(SignInRequest request)
        {
            const string failedAuthentication = "Credenciales incorrectas.";
            var user = await userRepository.FindByEmail(request.Email);

            if (user != null) return ResultBase<AuthenticateToken>.NOT_VALID(failedAuthentication);

            var passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if(!passwordValid) return ResultBase<AuthenticateToken>.NOT_VALID(failedAuthentication);

            var roles = await userRoleRepository.FindAllByUserId(user.Id);

            if(roles.Count == 0) return ResultBase<AuthenticateToken>.NOT_VALID(failedAuthentication);

            var roleConcat = "";
            foreach(var roleRole in roles)
            {
                roleConcat = roleConcat + roleRole.Role.Name;
            }

            var role = "";
            if (roleConcat.Contains(ConstantHelper.Role.ADMIN)) role = ConstantHelper.Role.ADMIN;
            else if(roleConcat.Contains(ConstantHelper.Role.CUSTOMER)) role = ConstantHelper.Role.CUSTOMER;

            var token = jwtService.GenerateToken(user, role);
            return ResultBase<AuthenticateToken>.COMPLET_RESULT(new AuthenticateToken(token));
        }

        public async Task<ResultBase<AuthenticateToken>> SignUp(SignUpRequest request)
        {
            var entity = request.ConvertToEntity();

            var gender = await personGenderRepository.FindById(request.PersonGenderId);

            if (gender is null) return ResultBase<AuthenticateToken>.NOT_FOUND("No se encontro el género de la persona.");

            var validEmail = await userRepository.FindByEmail(request.Email);

            if (validEmail != null) return ResultBase<AuthenticateToken>.NOT_VALID($"El correo \"{request.Email}\" esta siendo usado.");

            entity.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var role = await roleRepository.FindByName(ConstantHelper.Role.CUSTOMER);

            if (role is null) return ResultBase<AuthenticateToken>.NOT_FOUND("No se encontro el rol.");

            try
            {
                userRepository.Add(entity);
                await unitOfWork.CompleteAsync();
                userRoleRepository.Add(new UserRole { RoleId = role.Id, UserId = entity.Id });
                await unitOfWork.CompleteAsync();

                var roles = await userRoleRepository.FindAllByUserId(entity.Id);
                if (roles.Count == 0) return ResultBase<AuthenticateToken>.NOT_VALID("El usuario no tiene ningun tipo de rol.");

                var roleConcat = "";
                foreach (var roleRole in roles)
                {
                    roleConcat = roleConcat + roleRole.Role.Name;
                }

                var roleValue = "";
                if (roleConcat.Contains(ConstantHelper.Role.ADMIN)) roleValue = ConstantHelper.Role.ADMIN;
                else if (roleConcat.Contains(ConstantHelper.Role.CUSTOMER)) roleValue = ConstantHelper.Role.CUSTOMER;

                var token = jwtService.GenerateToken(entity, roleValue);
                return ResultBase<AuthenticateToken>.COMPLET_RESULT(new AuthenticateToken(token));
            }
            catch(Exception ex)
            {
                return ResultBase<AuthenticateToken>.CREATE_CATCH(ex.Message);
            }
        }
    }
}
