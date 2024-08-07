using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Communication.Responses.Common
{
    public class GetItemResponse<T> : AbstractResponseItem<T>
    {
        public GetItemResponse() { }
        public GetItemResponse(T item) : base(item) { }
        public GetItemResponse(Exception e) : base(e) { }
    }
}
