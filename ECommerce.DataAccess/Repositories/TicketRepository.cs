
using Dapper;
using ECommerce.Domain.Identity;
using System.Data;
using System.Data.Common;

namespace ECommerce.DataAccess.Repositories
{
    public class TicketRepository
         (EFApplicationContext context, AppDapperContext dapperContext, ILogger<TicketRepository> logger) 
        : ITicketRepository
    {
        private readonly IDbConnection dbConnection = dapperContext.CreateConnection();
        public async Task<int> CreateTicket(Ticket ticket)
        {
            ArgumentNullException.ThrowIfNull(ticket, nameof(ticket));
            await context.Tickets.AddAsync(ticket);
            logger.LogDebug($"new ticket has been CREATED with id '{ticket.Id}' from user with id '{ticket.ProviderId ?? ticket.CustomerId}'");
            return ticket.Id;
        }

        public async Task<int> DeleteTicket(int ticketId)
        {
            logger.LogDebug($"new ticket has been DELETED with id '{ticketId}'");
            await context.Tickets.Where(t => t.Id == ticketId).ExecuteDeleteAsync();
            return ticketId;
        }

        public async Task<int> EndTicket(int ticketId)
        {
            var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null)
                throw new EntityNotFoundException(typeof(Ticket), ticketId);
            ticket.IsCompleted = true;
            return ticket.Id;
        }

        public async Task<List<Ticket>> GetAll(bool? isCompleted, int page = 1, int size = 10)
        {
            string sqlQuery = "";
            if (isCompleted == null)
                sqlQuery = "Select * from Tickets Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
            else
                sqlQuery = "Select * from Tickets where IsCompleted = @isCompleted Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";

            var parameters = new { isCompleted, skip = (page - 1) * size, size };
            var reviews = await dbConnection.QueryAsync<Ticket>(sqlQuery, parameters);
            return reviews.ToList();
        }

        public async Task<Ticket> GetTicket(int ticketId)
        {
            var ticket = await context.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null)
                throw new EntityNotFoundException(typeof(Ticket), ticketId);
            return ticket;
        }
    }
}
