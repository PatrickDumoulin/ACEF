using CoreLib.Definitions;
using CoreLib.Injection;
using DataAccess.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Core.Definitions
{
    public abstract class AbstractBLL<TDAL> : IBLL where TDAL : IDAL
    {
        #region Members
        private TDAL dalinstance;
        private ProviderDALTypes dalType;

        protected TDAL dal
        {
            get
            {
                initializeDal();
                return dalinstance;
            }
        }

        private CultureInfo contextCulture;
        #endregion

        #region Constructors    
        public AbstractBLL() { }
        public AbstractBLL(ProviderDALTypes dalType)
        {
            this.dalType = dalType;
            dalinstance = GetDalInstance(dalType, null);
        }
        public AbstractBLL(IDAL dal)
        {
            this.dalinstance = GetDalFromExternalDal(dal);
        }
        public AbstractBLL(IDAL dal, ProviderDALTypes dalType)
        {
            this.dalType = dalType;
            this.dalinstance = GetDalFromExternalDal(dal, dalType);
        }
        #endregion

        #region Public methods
        public ITransaction BeginTransaction()
        {
            return dal.BeginTransaction();
        }

        public U GetBLL<U>() where U : IBLL
        {
            return GetBLL<U>(ProviderDALTypes.ENTITY);
        }
        public U GetBLL<U>(ProviderDALTypes dalType) where U : IBLL
        {
            initializeDal();
            return Injector.ImplementBll<U>(dalinstance, dalType);
        }
        #endregion

        protected CultureInfo ContextCulture
        {
            get
            {
                if (contextCulture == null)
                {
                    contextCulture = new CultureInfo("fr-CA");
                }
                return contextCulture;
            }
        }

        #region Private methods
        private void initializeDal()
        {
            if (dalinstance == null)
                dalinstance = GetDalInstance(dalType, null);
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dal.Dispose();
                }

                disposedValue = true;
            }
        }

        protected TDAL GetDalInstance(ProviderDALTypes dalType, IDAL externalDal)
        {
            return Injector.ImplementDal<TDAL>(dalType, externalDal);
        }
        protected TDAL GetDalFromExternalDal(IDAL externalDal)
        {
            IAcefDAL acefDAL = (IAcefDAL)externalDal;
            return acefDAL.GetDAL<TDAL>();
        }
        protected TDAL GetDalFromExternalDal(IDAL externalDal, ProviderDALTypes dalType)
        {
            IAcefDAL acefDAL = (IAcefDAL)externalDal;
            return acefDAL.GetDAL<TDAL>(dalType);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        #endregion

    }
}
