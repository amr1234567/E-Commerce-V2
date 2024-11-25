using ECommerce.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface IAdminRepository
    {
        Task<int> CreateAdmin(Admin admin);
    }
}
