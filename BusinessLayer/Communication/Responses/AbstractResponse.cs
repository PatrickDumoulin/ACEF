using BusinessLayer.Communication.Responses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Communication.Responses
{
    public abstract class AbstractResponse : IResponse
    {
        protected AbstractResponse()
        {
            Succeeded = true;
        }
        protected AbstractResponse(bool succeeded)
        {
            this.Succeeded = succeeded;
        }
        protected AbstractResponse(Exception e)
        {
            this.ErrorContext = e;
            this.Succeeded = false;
        }

        public bool Succeeded
        {
            get;
            set;
        }

        public Exception ErrorContext
        {
            get;
            set;
        }
    }
}
