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
    public class PersonGenderService : IPersonGenderService
    {
        private readonly IPersonGenderRepository personGenderRepository;
        private readonly IUnitOfWork unitOfWork;

        public PersonGenderService(IPersonGenderRepository personGenderRepository, IUnitOfWork unitOfWork)
        {
            this.personGenderRepository = personGenderRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ResultBase<PersonGenderResponse>> Create(ClaimsPrincipal claims, PersonGenderRequest request)
        {
            try
            {
                var entity = request.ConvertToEntity();
                personGenderRepository.Add(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<PersonGenderResponse>.COMPLET_RESULT(new PersonGenderResponse(entity));
            }catch(Exception ex)
            {
                return ResultBase<PersonGenderResponse>.CREATE_CATCH(ex.Message);
            }
        }

        public async Task<List<PersonGenderResponse>> FindAll(ClaimsPrincipal claims)
        {
            return await personGenderRepository.Queryable().Select(x => new PersonGenderResponse(x)).ToListAsync();
        }

        public async Task<ResultBase<PersonGenderResponse>> FindById(ClaimsPrincipal claims, int id)
        {
            var entity = await personGenderRepository.FindById(id);

            if (entity is null) return ResultBase<PersonGenderResponse>.NOT_FOUND("No se encontro el tipo de genero de la persona.");

            return ResultBase<PersonGenderResponse>.COMPLET_RESULT(new PersonGenderResponse(entity));
        }

        public Task<Pageable<PersonGenderResponse>> Pageable(ClaimsPrincipal claims, Page page)
        {
            var query = personGenderRepository.Queryable().Select(x => new PersonGenderResponse(x));
            return Pageable<PersonGenderResponse>.ConvertPageable(query, page);
        }

        public async Task<ResultBase<PersonGenderResponse>> Remove(ClaimsPrincipal claims, int id)
        {
            var entity = await personGenderRepository.FindById(id);

            if (entity is null) return ResultBase<PersonGenderResponse>.NOT_FOUND("No se encontro el tipo de genero de la persona.");

            try
            {
                personGenderRepository.Remove(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<PersonGenderResponse>.COMPLET_RESULT(new PersonGenderResponse(entity));
            }
            catch(Exception ex)
            {
                return ResultBase<PersonGenderResponse>.REMOVE_CATCH(ex.Message);
            }            
        }

        public async Task<List<ValueId>> Selector(ClaimsPrincipal claims)
        {
            return await personGenderRepository.Queryable().Select(x => new ValueId { Id = x.Id, Value = x.Name }).ToListAsync();
        }

        public async Task<ResultBase<PersonGenderResponse>> Update(ClaimsPrincipal claims, int id, PersonGenderRequest request)
        {
            var entity = await personGenderRepository.FindById(id);

            if (entity is null) return ResultBase<PersonGenderResponse>.NOT_FOUND("No se encontro el tipo de genero de la persona.");

            entity.Name = request.Name;
            try
            {
                personGenderRepository.Update(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<PersonGenderResponse>.COMPLET_RESULT(new PersonGenderResponse(entity));
            }
            catch (Exception ex)
            {
                return ResultBase<PersonGenderResponse>.UPDATE_CATCH(ex.Message);
            }
        }
    }
}
