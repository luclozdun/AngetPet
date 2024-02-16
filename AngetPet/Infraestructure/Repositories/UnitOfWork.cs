using AngetPet.Domain.Repository;

namespace AngetPet.Infraestructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AngetpetDbContext dbContext;

        public UnitOfWork(AngetpetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            dbContext.Database.CommitTransaction();
        }

        public async Task CompleteAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
