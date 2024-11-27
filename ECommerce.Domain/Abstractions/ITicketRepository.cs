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
        Task<int> EndTicket(int ticketId);
        Task<Ticket> GetTicket(int ticketId);
        Task<List<Ticket>> GetAll(bool? isCompleted, int page = 1, int size = 10);
    }
}
