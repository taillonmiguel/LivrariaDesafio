using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Shared.CustomException
{
    public interface ICustomExceptionHandler
    {
        Task Handler(System.Exception exception);
    }
}
