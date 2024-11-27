using ECommerce.Domain.Base;

namespace ECommerce.DataAccess.Repositories
{
    public class UserRepository
        (EFApplicationContext context,ILogger<UserRepository> logger)
        : IUserRepository
    {
        public async Task<int> BlockAccount(int id)
        {
            var account = await context.BaseUsers.FirstOrDefaultAsync(x => x.Id == id);
            if (account == null)
                throw new EntityNotFoundException(typeof(IdentityBase), id);
            account.IsBlocked = true;
            logger.LogInformation($"user with id '{id}' has been BLOCKED");
            return account.Id;
        }

        public async Task<IdentityBase> GetUserByEmail(string email)
        {
            var account = await context.BaseUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
            if (account == null)
                throw new EntityNotFoundException(typeof(IdentityBase), email);
            return account;
        }

        public async Task<IdentityBase> GetUserById(int id)
        {
            var account = await context.BaseUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (account == null)
                throw new EntityNotFoundException(typeof(IdentityBase), id);
            return account;
        }

        public async Task<IdentityBase> GetUserByCriteria(Func<IdentityBase, bool> criteria)
        {
            var account = await context.BaseUsers.AsNoTracking().FirstOrDefaultAsync(x => criteria(x));
            if (account == null)
                throw new EntityNotFoundException(typeof(IdentityBase), nameof(GetUserByCriteria));
            return account;
        }

        public Task<int> UpdateAllWithFunc(Action<IdentityBase> action)
        {
            foreach(var user in context.BaseUsers)
                action(user);
            return Task.FromResult(1);
        }

        public async Task<int> UpdateByCriteriaWithFunc(Func<IdentityBase, bool> criteria, Action<IdentityBase> action)
        {
            var user = await context.BaseUsers.FirstOrDefaultAsync(u => criteria(u));
            if (user == null)
                throw new EntityNotFoundException(typeof(IdentityBase), nameof(UpdateByCriteriaWithFunc));
            action(user);
            return 1;
        }

        public async Task<IdentityBase> UpdateUser(IdentityBase identity)
        {
            await context.BaseUsers.Where(u => u.Id == identity.Id).ExecuteUpdateAsync(user =>
            user
            .SetProperty(p => p.Password, identity.Password)
            .SetProperty(p => p.RefreshToken, identity.RefreshToken)
            .SetProperty(p => p.Name, identity.Name)
            .SetProperty(p => p.Role, identity.Role)
            );
            logger.LogInformation($"user with id '{identity.Id}' has been UPDATED");
            return identity;
        }
    }
}
