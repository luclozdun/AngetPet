namespace AngetPet.Domain.Repository
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void BeginTransaction();
        void CommitTransaction();
    }
}
