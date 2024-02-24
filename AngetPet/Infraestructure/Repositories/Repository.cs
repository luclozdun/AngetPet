using AngetPet.Domain.Models;
using AngetPet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AngetPet.Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly AngetpetDbContext context;

        public Repository(AngetpetDbContext context)
        {
            this.context = context;
        }        

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        public async Task<List<T>> FindAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> FindById(int id)
        {
            return await context.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable<T> Queryable()
        {
            return context.Set<T>().AsQueryable();
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void RemoveRange(List<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public void UpdateRange(List<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
        }
    }
}
