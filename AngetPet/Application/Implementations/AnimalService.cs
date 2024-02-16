using AngetPet.Application.Dto;
using AngetPet.Application.Service;
using AngetPet.Domain.Objects;
using AngetPet.Domain.Repositories;
using AngetPet.Domain.Repository;
using AngetPet.Shared.Helpers;
using AngetPet.Shared.Paginator;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AngetPet.Application.Implementation
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository animalRepository;
        private readonly IUnitOfWork unitOfWork;

        public AnimalService(IAnimalRepository animalRepository, IUnitOfWork unitOfWork)
        {
            this.animalRepository = animalRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ResultBase<AnimalResponse>> Create(ClaimsPrincipal claims, AnimalRequest request)
        {
            var entity = request.ConvertToEntity();

            try
            {
                animalRepository.Add(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<AnimalResponse>.COMPLET_RESULT(AnimalResponse.ConvertToEntity(entity));
            }catch(Exception ex)
            {
                return ResultBase<AnimalResponse>.CREATE_CATCH(ex.Message);
            }
        }

        public async Task<List<AnimalResponse>> FindAll(ClaimsPrincipal claims)
        {
            return await animalRepository.Queryable().Select(x => new AnimalResponse(x)).ToListAsync();
        }

        public async Task<ResultBase<AnimalResponse>> FindById(ClaimsPrincipal claims, int id)
        {
            var entity = await animalRepository.FindById(id);

            if (entity is null) return ResultBase<AnimalResponse>.NOT_FOUND("No se encontro el animal.");

            return ResultBase<AnimalResponse>.COMPLET_RESULT(AnimalResponse.ConvertToEntity(entity));
        }

        public async Task<Pageable<AnimalResponse>> Pageable(ClaimsPrincipal claims, Page page)
        {
            var query = animalRepository.Queryable().Select(x => new AnimalResponse(x));

            return await Pageable<AnimalResponse>.ConvertPageable(query, page);
        }

        public async Task<ResultBase<AnimalResponse>> Remove(ClaimsPrincipal claims, int id)
        {
            var entity = await animalRepository.FindById(id);

            if (entity is null) return ResultBase<AnimalResponse>.NOT_FOUND("No se encontro el animal.");

            try
            {
                animalRepository.Remove(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<AnimalResponse>.COMPLET_RESULT(new AnimalResponse(entity));
            }
            catch(Exception ex) {
                return ResultBase<AnimalResponse>.REMOVE_CATCH(ex.Message);
            }
        }

        public async Task<List<ValueId>> Selector(ClaimsPrincipal claims)
        {
            return await animalRepository.Queryable().Select(x => new ValueId { Id = x.Id, Value = x.Name }).ToListAsync();
        }

        public async Task<ResultBase<AnimalResponse>> Update(ClaimsPrincipal claims, int id, AnimalRequest request)
        {
            var entity = await animalRepository.FindById(id);

            if (entity is null) return ResultBase<AnimalResponse>.NOT_FOUND("No se encontro el animal.");

            try
            {
                entity.Name = request.Name;
                entity.Image = request.Image;
                entity.Icon = request.Icon;
                animalRepository.Update(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<AnimalResponse>.COMPLET_RESULT(AnimalResponse.ConvertToEntity(entity));
            }catch(Exception e)
            {
                return ResultBase<AnimalResponse>.UPDATE_CATCH(e.Message);
            }
        }
    }
}
