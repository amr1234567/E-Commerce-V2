using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Models;

namespace ECommerce.DataAccess.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public Task<int> CreateTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteTicket(int ticketId)
        {
            throw new NotImplementedException();
        }
    }
}
