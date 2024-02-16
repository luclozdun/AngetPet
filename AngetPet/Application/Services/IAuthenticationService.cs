using AngetPet.Application.Dtos;
using AngetPet.Application.Service;
using AngetPet.Shared.Helpers;
using static AngetPet.Application.Dtos.SignUpRequest;

namespace AngetPet.Application.Services
{
    public interface IAuthenticationService
    {
        Task<ResultBase<AuthenticateToken>> SignIn(SignInRequest request);
        Task<ResultBase<AuthenticateToken>> SignUp(SignUpRequest request);
    }
}
