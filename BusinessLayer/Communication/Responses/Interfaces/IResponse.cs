using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Communication.Responses.Interfaces
{
    public interface IResponse
    {
        bool Succeeded { get; set; }

        Exception ErrorContext { get; set; }
    }
}
