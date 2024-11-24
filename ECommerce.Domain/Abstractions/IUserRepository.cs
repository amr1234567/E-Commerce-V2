using ECommerce.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<IdentityBase> GetUserById(int id);
        Task<IdentityBase> GetUserByEmail(string email);
        Task<IdentityBase> UpdateUser(string? newPassword, string? newName);
        Task<int> BlockAccount(int id);
    }
}
