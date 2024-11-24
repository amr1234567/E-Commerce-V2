using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        public Task<Admin> CreateAdmin(Admin admin)
        {
            throw new NotImplementedException();
        }
    }
}
