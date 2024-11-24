using ECommerce.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Models;
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
