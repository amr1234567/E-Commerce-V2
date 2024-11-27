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
        Task<IdentityBase> UpdateUser(IdentityBase identityBase);
        Task<int> UpdateAllWithFunc(Action<IdentityBase> action);
        Task<int> BlockAccount(int id);
        Task<int> UpdateByCriteriaWithFunc(Func<IdentityBase,bool> predicate, Action<IdentityBase> action);
        Task<IdentityBase> GetUserByCriteria(Func<IdentityBase, bool> criteria);
    }
}
