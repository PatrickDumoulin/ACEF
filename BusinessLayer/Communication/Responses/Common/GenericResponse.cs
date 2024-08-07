using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Communication.Responses.Common
{
    public class GenericResponse : AbstractResponse
    {
        public GenericResponse() :base() { }
        public GenericResponse(bool succeeded) : base(succeeded) { }
        public GenericResponse(Exception e) : base(e) { }
    }
}
