namespace ECommerce.DataAccess.Repositories
{
    public class UnitOfWork(EFApplicationContext context) : IUnitOfWork
    {
        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
