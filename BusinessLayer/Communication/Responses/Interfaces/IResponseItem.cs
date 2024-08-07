using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Communication.Responses.Interfaces
{
    public interface IResponseItem<T> : IResponse
    {
        T Element { get; }
    }
}
