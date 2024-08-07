using BusinessLayer.Communication.Responses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Communication.Responses
{
    public abstract class AbstractResponseItem<T> : AbstractResponse, IResponseItem<T>
    {
        public AbstractResponseItem() { }
        public AbstractResponseItem(T element)
        {
            Element = element;
        }
        public AbstractResponseItem(Exception e) : base(e) { }

        public T Element
        {
            get;
            private set;
        }
    }
}
