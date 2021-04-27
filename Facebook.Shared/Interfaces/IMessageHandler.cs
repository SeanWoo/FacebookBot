using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Shared.Interfaces
{
    public interface IMessageHandler
    {
        void Run(string[] args);
    }
}
