using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Communication.Responses.Common
{
    public class GetListResponse<T> : AbstractResponseList<T>
    {
        public GetListResponse() { }
        public GetListResponse(List<T> items) : base(items) { }
        public GetListResponse(Exception e) : base(e) { }
    }
}
