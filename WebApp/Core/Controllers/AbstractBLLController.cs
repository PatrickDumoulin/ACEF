using CoreLib.Definitions;
using CoreLib.Injection;

namespace WebApp.Core.Controllers
{
    public abstract class AbstractBLLController<T> : AbstractController where T : IBLL
    {
        protected T bll { get; private set; }

        public AbstractBLLController()
        {
            initializeBll();
        }

        protected TBLL GetBLL<TBLL>(ProviderDALTypes dALTypes) where TBLL : IBLL
        {
            TBLL createdBll = bll.GetBLL<TBLL>(dALTypes);
            return createdBll;
        }

        protected TBLL GetBLL<TBLL>() where TBLL : IBLL
        {
            TBLL createdBll = bll.GetBLL<TBLL>();
            return createdBll;
        }

        private void initializeBll()
        {
            if (bll == null)
                bll = Injector.ImplementBll<T>();
           
        }
    }
}
