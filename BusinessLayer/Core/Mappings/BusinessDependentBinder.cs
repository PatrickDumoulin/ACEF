using CoreLib.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Core.Mappings
{
    public abstract class BusinessDependentBinder : BaseBinder, IDependentBinder
    {
        private BaseBinder businessBinder;

        public BusinessDependentBinder()
        {
            businessBinder = new ProdBinder();
        }

        public BaseBinder ReferencedBinder
        {
            get { return businessBinder; }
        }

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                businessBinder.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
