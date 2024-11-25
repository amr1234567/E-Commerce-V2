using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Constants
{
    public class CachingConfiguration
    {
        private static TimeSpan PeriodToDelete = TimeSpan.FromHours(24);
        public static readonly DistributedCacheEntryOptions CachingConfigurationParameter =
            new()
            {
                AbsoluteExpirationRelativeToNow = PeriodToDelete
            };
    }
}
