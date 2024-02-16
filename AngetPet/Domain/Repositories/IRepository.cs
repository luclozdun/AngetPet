namespace AngetPet.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Queryable();
        Task<List<T>> FindAll();
        Task<T> FindById(int id);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        void AddRange(List<T> entities);
        void RemoveRange(List<T> entities);
        void UpdateRange(List<T> entities);
    }
}
