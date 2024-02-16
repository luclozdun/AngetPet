using AngetPet.Application.Dto;
using AngetPet.Application.Dtos;
using AngetPet.Application.Services;
using AngetPet.Domain.Models;
using AngetPet.Domain.Objects;
using AngetPet.Domain.Repositories;
using AngetPet.Domain.Repository;
using AngetPet.Shared.Helpers;
using AngetPet.Shared.Paginator;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace AngetPet.Application.Implementations
{
    public class PetService : IPetService
    {
        private readonly IPetRepository petRepository;
        private readonly IUnitOfWork unitOfWork;

        public PetService(IPetRepository petRepository, IUnitOfWork unitOfWork)
        {
            this.petRepository = petRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ResultBase<PetResponse>> Create(ClaimsPrincipal claims, PetRequest request)
        {
            try
            {
                var entity = request.ConvertToEntity();
                entity.Created = DateTime.Now;
                petRepository.Add(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<PetResponse>.COMPLET_RESULT(new PetResponse(entity));
            }catch(Exception ex)
            {
                return ResultBase<PetResponse>.CREATE_CATCH(ex.Message);
            }
        }

        public async Task<List<PetResponse>> FindAll(ClaimsPrincipal claims)
        {
            return await petRepository.Queryable().Include(x => x.Size).Include(x => x.Animal).Include(x => x.Gender).Select(x => new PetResponse(x)).ToListAsync();
        }

        public async Task<ResultBase<PetResponse>> FindById(ClaimsPrincipal claims, int id)
        {
            var entity = await petRepository.Queryable().Include(x => x.Size).Include(x => x.Animal).Include(x => x.Gender).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (entity is null) return ResultBase<PetResponse>.NOT_FOUND("No se encontro la mascota.");

            return ResultBase<PetResponse>.COMPLET_RESULT(new PetResponse(entity));
        }

        public Task<Pageable<PetResponse>> Pageable(ClaimsPrincipal claims, Page page)
        {
            var query = petRepository.Queryable().Include(x => x.Size).Include(x => x.Animal).Include(x => x.Gender).Select(x => new PetResponse(x)).AsQueryable();
            return Pageable<PetResponse>.ConvertPageable(query, page);
        }

        public async Task<ResultBase<PetResponse>> Remove(ClaimsPrincipal claims, int id)
        {
            var entity = await petRepository.FindById(id);

            if (entity is null) return ResultBase<PetResponse>.NOT_FOUND("No se encontro la mascota.");

            try
            {
                petRepository.Remove(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<PetResponse>.COMPLET_RESULT(new PetResponse(entity));
            }
            catch(Exception e)
            {
                return ResultBase<PetResponse>.REMOVE_CATCH(e.Message);
            }            
        }

        public async Task<List<ValueId>> Selector(ClaimsPrincipal claims)
        {
            return await petRepository.Queryable().Select(x => new ValueId { Id=x.Id, Value = x.Name}).ToListAsync();
        }

        public async Task<ResultBase<PetResponse>> Update(ClaimsPrincipal claims, int id, PetRequest request)
        {
            var entity = await petRepository.FindById(id);

            if (entity is null) return ResultBase<PetResponse>.NOT_FOUND("No se encontro la mascota.");

            entity.Name = request.Name;
            entity.SubDescription = request.SubDescription;
            entity.IsAntiparasiticVaccine = request.IsAntiparasiticVaccine;
            entity.DateAntiparasiticVaccine = request.DateAntiparasiticVaccine;
            entity.Birthdate = request.Birthdate;
            entity.Description = request.Description;
            entity.Characteristic = request.Characteristic;
            entity.Image = request.Image;
            entity.AnimalId = request.AnimalId;
            entity.GenderId = request.GenderId;
            entity.SizeId = request.SizeId;
            entity.IsSterilized = request.IsSterilized;
            entity.IsTrained = request.IsTrained;
            entity.TextColor = request.TextColor;
            entity.BackgroundColor = request.BackgroundColor;
            entity.BackgroundColorIcon = request.BackgroundColorIcon;
            entity.Updated = DateTime.Now;
            try
            {
                petRepository.Update(entity);
                await unitOfWork.CompleteAsync();
                return ResultBase<PetResponse>.COMPLET_RESULT(new PetResponse(entity));
            }
            catch (Exception e)
            {
                return ResultBase<PetResponse>.UPDATE_CATCH(e.Message);
            }
        }
    }
}
