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
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository skillRepository;
        private readonly IUnitOfWork unitOfWork;

        public SkillService(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
        {
            this.skillRepository = skillRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ResultBase<SkillResponse>> Create(ClaimsPrincipal claims, SkillRequest request)
        {
            try
            {
                var entity = request.ConvertToEntity();
                skillRepository.Add(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<SkillResponse>.COMPLET_RESULT(new SkillResponse(entity));
            }catch(Exception ex)
            {
                return ResultBase<SkillResponse>.CREATE_CATCH(ex.Message);
            }
        }

        public Task<List<SkillResponse>> FindAll(ClaimsPrincipal claims)
        {
            return skillRepository.Queryable().Select(x => new SkillResponse(x)).ToListAsync();
        }

        public async Task<ResultBase<SkillResponse>> FindById(ClaimsPrincipal claims, int id)
        {
            var entity = await skillRepository.FindById(id);

            if (entity is null) return ResultBase<SkillResponse>.NOT_FOUND("No se encontro la habilidad.");

            return ResultBase<SkillResponse>.COMPLET_RESULT(new SkillResponse(entity));
        }

        public Task<Pageable<SkillResponse>> Pageable(ClaimsPrincipal claims, Page page)
        {
            var query = skillRepository.Queryable().Select(x => new SkillResponse(x));
            return Pageable<SkillResponse>.ConvertPageable(query, page);
        }

        public async Task<ResultBase<SkillResponse>> Remove(ClaimsPrincipal claims, int id)
        {
            var entity = await skillRepository.FindById(id);

            if (entity is null) return ResultBase<SkillResponse>.NOT_FOUND("No se encontro la habilidad.");

            try
            {
                skillRepository.Remove(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<SkillResponse>.COMPLET_RESULT(new SkillResponse(entity));
            }
            catch (Exception ex)
            {
                return ResultBase<SkillResponse>.REMOVE_CATCH(ex.Message);
            }
        }

        public async Task<List<ValueId>> Selector(ClaimsPrincipal claims)
        {
            return await skillRepository.Queryable().Select(x => new ValueId { Id = x.Id, Value = x.Name }).ToListAsync();
        }

        public async Task<ResultBase<SkillResponse>> Update(ClaimsPrincipal claims, int id, SkillRequest request)
        {
            var entity = await skillRepository.FindById(id);

            if (entity is null) return ResultBase<SkillResponse>.NOT_FOUND("No se encontro la habilidad.");

            entity.Name = request.Name;
            entity.Icon = request.Icon;
            entity.Description = request.Description;
            try
            {
                skillRepository.Update(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<SkillResponse>.COMPLET_RESULT(new SkillResponse(entity));
            }
            catch (Exception ex)
            {
                return ResultBase<SkillResponse>.UPDATE_CATCH(ex.Message);
            }
        }
    }
}
