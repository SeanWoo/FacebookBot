using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facebook.Shared.Interfaces
{
    public interface IMessageHandler
    {
        Task Run(string[] args, CancellationToken cancellationToken);
    }
}
