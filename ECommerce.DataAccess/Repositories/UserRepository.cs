using ECommerce.Domain.Base;

namespace ECommerce.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<int> BlockAccount(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityBase> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityBase> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityBase> UpdateUser(string? newPassword, string? newName)
        {
            throw new NotImplementedException();
        }
    }
}
