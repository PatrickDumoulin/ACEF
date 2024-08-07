using CoreLib.Definitions;
using CoreLib.Injection;
using DataAccess.Models;
using DataModels.Mappings;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataAccess.Core.Definitions
{
    public abstract class AcefEntityDAL : AbstractEntityDAL<Projet_CLContext>, IAcefDAL
    {
        #region Members
        protected string originalState;
        #endregion

        #region Constructors
        public AcefEntityDAL() : base() { }
        public AcefEntityDAL(AcefEntityDAL externalDal): base(externalDal) { }
        #endregion

        #region Properties
        protected override void OnInitializeConnection(AbstractEntityDAL<Projet_CLContext> externalEntityDal)
        {
            AcefEntityDAL dal = (AcefEntityDAL)externalEntityDal;
            //transactions = aleopDal.Transactions;
        }

        protected override DatabaseTypes DatabaseType
        {
            get
            {
                return DatabaseTypes.Project_CL;
            }
        }
        #endregion

        #region Public Methods
        public void SavePendingChanges()
        {
            Db.SaveChanges();
        }

        /*
        public Byte[] HashValue(string value, string key)
        {
            return Db.HashValue(value, key);
        }

        public Byte[] EncryptValue(string value, string key)
        {
            return Db.EncryptValue(value, key);
        }
        public string DecryptValue(Byte[] value, string key)
        {
            return Db.DecryptValue(value, key);
        }

        public int DecryptId(IIdentifierToDecrypt model)
        {
            return DecryptId(model.EncryptedID);
        }
        public int DecryptId(string encryptedId)
        {
            return Convert.ToInt32(DecryptFromFriendlyUrl(encryptedId));
        }

        public Byte[] EncryptToken(string value)
        {
            return Db.EncryptToken(value);
        }
        public string DecryptToken(Byte[] value)
        {
            return Db.DecryptToken(value);
        }

        public Byte[] GetByteArrayFromHexaString(string value)
        {

            return Db.GetByteArrayFromHexaString(value);
        }
        public string GetHexaStringFromByteArray(Byte[] value)
        {
            return Db.GetHexaStringFromByteArray(value);

        }

        public string EncryptToFriendlyUrl(string data)
        {
            return Db.EncryptToFriendlyUrl(data);
        }
        public string DecryptFromFriendlyUrl(string data)
        {
            return Db.DecryptFromFriendlyUrl(data);
        }*/


        public T GetDAL<T>() where T : IDAL
        {
            return GetDAL<T>(ProviderDALTypes.ENTITY);
        }
        public T GetDAL<T>(ProviderDALTypes dalType) where T : IDAL
        {
            var otherDal = Injector.ImplementDal<T>(dalType, this);

            if (otherDal is IDbContextDAL<Projet_CLContext>)
            {
                IDbContextDAL<Projet_CLContext> externalEntityDal = (otherDal as IDbContextDAL<Projet_CLContext>);
                addChildDal(externalEntityDal);
                externalEntityDal.InitializeConnection(this);
            }

            return otherDal;
        }

        public int BulkSaveRecords(List<IBOL> bols)
        {
            return this.SaveEntities(bols);
        }
        #endregion
    }
}
