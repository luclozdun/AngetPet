using AngetPet.Domain.Objects;
using AngetPet.Shared.Helpers;
using AngetPet.Shared.Paginator;
using System.Security.Claims;

namespace AngetPet.Application.Service
{
    public interface IService<T, Request, Response> where Response : class 
    {
        Task<Pageable<Response>> Pageable(ClaimsPrincipal claims,Page page);
        Task<List<Response>> FindAll(ClaimsPrincipal claims);
        Task<ResultBase<Response>> FindById(ClaimsPrincipal claims, int id);
        Task<ResultBase<Response>> Create(ClaimsPrincipal claims, Request request);
        Task<ResultBase<Response>> Update(ClaimsPrincipal claims, int id, Request request);
        Task<ResultBase<Response>> Remove(ClaimsPrincipal claims, int id);
        Task<List<ValueId>> Selector(ClaimsPrincipal claims);
    }
}
