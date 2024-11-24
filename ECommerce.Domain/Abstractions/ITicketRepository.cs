using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface ITicketRepository
    {
        Task<int> CreateTicket(Ticket ticket);
        Task<int> DeleteTicket(int ticketId);
    }
}
