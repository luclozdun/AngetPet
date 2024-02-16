using AngetPet.Application.Dtos;
using AngetPet.Application.Services;
using AngetPet.Domain.Models;
using AngetPet.Domain.Objects;
using AngetPet.Domain.Repositories;
using AngetPet.Domain.Repository;
using AngetPet.Shared.Helpers;
using AngetPet.Shared.Paginator;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AngetPet.Application.Implementations
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository sizeRepository;
        private readonly IUnitOfWork unitOfWork;

        public SizeService(ISizeRepository sizeRepository, IUnitOfWork unitOfWork)
        {
            this.sizeRepository = sizeRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ResultBase<SizeResponse>> Create(ClaimsPrincipal claims, SizeRequest request)
        {
            try
            {
                var entity = request.ConvertToEntity();
                sizeRepository.Add(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<SizeResponse>.COMPLET_RESULT(new SizeResponse(entity));
            }catch(Exception ex)
            {
                return ResultBase<SizeResponse>.CREATE_CATCH(ex.Message);
            }
        }

        public async Task<List<SizeResponse>> FindAll(ClaimsPrincipal claims)
        {
            return await sizeRepository.Queryable().Select(x => new SizeResponse(x)).ToListAsync();
        }

        public async Task<ResultBase<SizeResponse>> FindById(ClaimsPrincipal claims, int id)
        {
            var entity = await sizeRepository.FindById(id);

            if (entity is null) return ResultBase<SizeResponse>.NOT_FOUND("No se encontro el tamaño.");

            return ResultBase<SizeResponse>.COMPLET_RESULT(new SizeResponse(entity));
        }

        public Task<Pageable<SizeResponse>> Pageable(ClaimsPrincipal claims, Page page)
        {
            var query = sizeRepository.Queryable().Select(x => new SizeResponse(x));
            return Pageable<SizeResponse>.ConvertPageable(query, page);
        }

        public async Task<ResultBase<SizeResponse>> Remove(ClaimsPrincipal claims, int id)
        {
            var entity = await sizeRepository.FindById(id);

            if (entity is null) return ResultBase<SizeResponse>.NOT_FOUND("No se encontro el tamaño.");

            try
            {
                sizeRepository.Remove(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<SizeResponse>.COMPLET_RESULT(new SizeResponse(entity));
            }catch(Exception e)
            {
                return ResultBase<SizeResponse>.REMOVE_CATCH(e.Message);
            }
        }

        public async Task<List<ValueId>> Selector(ClaimsPrincipal claims)
        {
            return await sizeRepository.Queryable().Select(x => new ValueId { Id = x.Id, Value=x.Name }).ToListAsync();
        }

        public async Task<ResultBase<SizeResponse>> Update(ClaimsPrincipal claims, int id, SizeRequest request)
        {
            var entity = await sizeRepository.FindById(id);

            if (entity is null) return ResultBase<SizeResponse>.NOT_FOUND("No se encontro el tamaño.");

            entity.Name = request.Name;
            entity.Description = request.Description;
            try
            {
                sizeRepository.Update(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<SizeResponse>.COMPLET_RESULT(new SizeResponse(entity));
            }
            catch (Exception e)
            {
                return ResultBase<SizeResponse>.UPDATE_CATCH(e.Message);
            }
        }
    }
}
