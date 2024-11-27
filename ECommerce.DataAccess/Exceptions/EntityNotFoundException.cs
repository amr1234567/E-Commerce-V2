using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityType, object identifier) : base($"Entity of type {entityType} with identifier : '{identifier}' NOT FOUND") 
        { }
        public EntityNotFoundException(Type entityType, object identifier) : base($"Entity of type {entityType.Name} with identifier : '{identifier}' NOT FOUND")
        { }
        public EntityNotFoundException(Type entityType, string methodName) : base($"Entity of type {entityType.Name} was NOT FOUND in method : '{methodName}'")
        { }
    }
}
