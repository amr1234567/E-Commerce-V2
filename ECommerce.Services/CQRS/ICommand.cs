using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.CQRS
{
    public interface ICommand: ICommandBase
    {
    }
    public interface ICommand<Response>: ICommandBase
    {
    }

    public interface ICommandBase
    {
    }
}
