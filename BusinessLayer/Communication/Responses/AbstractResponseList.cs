using BusinessLayer.Communication.Responses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Communication.Responses
{
    public abstract class AbstractResponseList<T> : AbstractResponse, IResponseList<T>
    {
        public AbstractResponseList() { }
        public AbstractResponseList(List<T> elementList)
        {
            ElementList = elementList;
        }
        public AbstractResponseList(Exception e) : base(e) { }

        public List<T> ElementList
        {
            get;
            private set;
        }
    }
}
