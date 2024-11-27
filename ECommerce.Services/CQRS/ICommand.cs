using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.CQRS
{
    public interface ICommand : ICommandBase, IRequest
    {
    }

    public interface ICommand<TResponse> : ICommandBase, IRequest<TResponse>
    {
    }

    public interface ICommandBase
    {
    }
}
