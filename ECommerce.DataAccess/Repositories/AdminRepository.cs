using ECommerce.DataAccess.EFContext;
using ECommerce.DataAccess.Exceptions;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    public class AdminRepository
        (EFApplicationContext context,IUserRepository userRepository, ILogger<AdminRepository> logger)
        : IAdminRepository
    {
        public async Task<int> CreateAdmin(Admin model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));
            var admin = await userRepository.GetUserByEmail(model.Email);
            if (admin != null)
            {
                throw new EntityExistsException(typeof(Admin).Name, model.Email);
            }

            await context.Admins.AddAsync(model);
            logger.LogInformation($"New Admin has been GENERATED with id '{model.Id}' and email '{model.Email}'");
            return 1;
        }
    }
}
