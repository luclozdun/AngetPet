using AngetPet.Application.Dtos;
using AngetPet.Application.Services;
using AngetPet.Domain.Objects;
using AngetPet.Domain.Repositories;
using AngetPet.Domain.Repository;
using AngetPet.Shared.Helpers;
using AngetPet.Shared.Paginator;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AngetPet.Application.Implementations
{
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository genderRepository;
        private readonly IUnitOfWork unitOfWork;

        public GenderService(IGenderRepository genderRepository, IUnitOfWork unitOfWork)
        {
            this.genderRepository = genderRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ResultBase<GenderResponse>> Create(ClaimsPrincipal claims, GenderRequest request)
        {
            try
            {
                var entity = request.ConvertToEntity();
                genderRepository.Add(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<GenderResponse>.COMPLET_RESULT(new GenderResponse(entity));
            }catch (Exception ex)
            {
                return ResultBase<GenderResponse>.CREATE_CATCH(ex.Message);
            }
        }

        public async Task<List<GenderResponse>> FindAll(ClaimsPrincipal claims)
        {
            return await genderRepository.Queryable().Select(x => new GenderResponse(x)).ToListAsync();
        }

        public async Task<ResultBase<GenderResponse>> FindById(ClaimsPrincipal claims, int id)
        {
            var entity = await genderRepository.FindById(id);

            if (entity is null) return ResultBase<GenderResponse>.NOT_FOUND("No se encontro el genero.");

            return ResultBase<GenderResponse>.COMPLET_RESULT(new GenderResponse(entity));
        }

        public async Task<Pageable<GenderResponse>> Pageable(ClaimsPrincipal claims, Page page)
        {
            var query = genderRepository.Queryable().Select(x => new GenderResponse(x));
            return await Pageable<GenderResponse>.ConvertPageable(query, page);
        }

        public async Task<ResultBase<GenderResponse>> Remove(ClaimsPrincipal claims, int id)
        {
            var entity = await genderRepository.FindById(id);

            if (entity is null) return ResultBase<GenderResponse>.NOT_FOUND("No se encontro el genero.");

            try
            {
                genderRepository.Remove(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<GenderResponse>.COMPLET_RESULT(new GenderResponse(entity));
            }
            catch(Exception ex) {
                return ResultBase<GenderResponse>.REMOVE_CATCH(ex.Message);
            }            
        }

        public async Task<List<ValueId>> Selector(ClaimsPrincipal claims)
        {
            return await genderRepository.Queryable().Select(x => new ValueId { Id = x.Id, Value = x.Name }).ToListAsync();
        }

        public async Task<ResultBase<GenderResponse>> Update(ClaimsPrincipal claims, int id, GenderRequest request)
        {
            var entity = await genderRepository.FindById(id);

            if (entity is null) return ResultBase<GenderResponse>.NOT_FOUND("No se encontro el genero.");

            entity.Name = request.Name;
            try
            {
                genderRepository.Update(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<GenderResponse>.COMPLET_RESULT(new GenderResponse(entity));
            }
            catch (Exception ex)
            {
                return ResultBase<GenderResponse>.UPDATE_CATCH(ex.Message);
            }
        }
    }
}
