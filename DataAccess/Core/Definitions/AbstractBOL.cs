using CoreLib.Definitions;
using DataAccess.Providers;
using DataModels.Definitions;
using DataModels.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Core.Definitions
{
    public abstract class AbstractBOL<T> : IBOL
        where T : IRecord, new()
    {
        #region Private Members
        private EntityDALMappingFactoryWrapper mapper = new EntityDALMappingFactoryWrapper();

        private Dictionary<string, decimal> cachedDecryptedValues;
        private Dictionary<string, PropertyInfo> cachedPropertyInfoList;

        #endregion

        #region Constructor
        public AbstractBOL()
        {
            SubBols = new Dictionary<Type, object>();
            SubBolsV = new List<ICollection<IRecord>>();

            this.Record = new T();

            cachedDecryptedValues = new Dictionary<string, decimal>();
            cachedPropertyInfoList = new Dictionary<string, PropertyInfo>();

            if (this.Record is ISequenced)
                ((ISequenced)Record).Id = GenerateID((ISequenced)Record);

            this.State = ObjectState.ADDED;
        }
        public AbstractBOL(T record)
        {
            SubBols = new Dictionary<Type, object>();
            SubBolsV = new List<ICollection<IRecord>>();

            cachedDecryptedValues = new Dictionary<string, decimal>();
            cachedPropertyInfoList = new Dictionary<string, PropertyInfo>();

            this.Record = record;
            this.State = ObjectState.MODIFIED;
        }
        #endregion

        #region Members

        #endregion;

        #region Properties
        public T Record { get; private set; }
        public IRecord UntypedRecord { get { return Record; } }

        public List<ICollection<IRecord>> SubBolsV { get; private set; }
        public ObjectState State { get; set; }

        public Dictionary<Type, object> SubBols { get; private set; }
        public IBOL ParentBol { get; private set; }
        #endregion

        #region Public Methods
        public byte[] EncryptValue(string data)
        {
            return DBProviderHelper.EncryptValue(data, (IIdentifierEntity)Record);
        }
        public string DecryptValue(byte[] encryptedData)
        {
            return DBProviderHelper.DecryptValue(encryptedData, (IIdentifierEntity)Record);
        }

        public byte[] EncryptNumericValue(decimal data)
        {
            return EncryptValue(data.Normalize().ToString(DBProviderHelper.NumericEncryptedCulture));
        }

        public void EncryptNumericValue(Expression<Func<T, byte[]>> encryptedData, decimal value)
        {
            MemberExpression body = encryptedData.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)encryptedData.Body;
                body = ubody.Operand as MemberExpression;
            }

            string fieldName = body.Member.Name;
            if (!cachedDecryptedValues.ContainsKey(fieldName))
            {
                cachedDecryptedValues.Add(fieldName, 0);
            }
            cachedDecryptedValues[fieldName] = value;

            byte[] encryptedRawData = EncryptNumericValue(value);

            if (!cachedPropertyInfoList.ContainsKey(fieldName))
            {
                Type recordType = Record.GetType();
                PropertyInfo newPropertyInfo = recordType.GetProperty(fieldName);
                cachedPropertyInfoList.Add(fieldName, newPropertyInfo);
            }

            PropertyInfo propertyInfo = cachedPropertyInfoList[fieldName];
            propertyInfo.SetValue(Record, encryptedRawData);
        }

        public decimal DecryptNumericValue(byte[] encryptedData)
        {
            return Convert.ToDecimal(DecryptValue(encryptedData), DBProviderHelper.NumericEncryptedCulture).Normalize();
        }

        public decimal DecryptNumericValue(Expression<Func<T, byte[]>> encryptedData)
        {
            MemberExpression body = encryptedData.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)encryptedData.Body;
                body = ubody.Operand as MemberExpression;
            }

            string fieldName = body.Member.Name;
            if (!cachedDecryptedValues.ContainsKey(fieldName))
            {
                var func = encryptedData.Compile();
                var encryptedRawData = func(Record);
                decimal decryptedValue = DecryptNumericValue(encryptedRawData);
                cachedDecryptedValues.Add(fieldName, decryptedValue);
            }

            return cachedDecryptedValues[fieldName];
        }

        public TBol GetParentBol<TBol>(IRecord record)
            where TBol : IBOL, new()
        {
            if (record == null)
                return default(TBol);

            ParentBol = mapper.NewBol<TBol>(record);

            return (TBol)ParentBol;
        }

        public TBol GetSubBol<TBol>(IRecord record)
            where TBol : IBOL, new()
        {
            if (record == null)
                return default(TBol);

            Type bolType = typeof(TBol);

            if (!SubBols.ContainsKey(bolType))
                SubBols.Add(bolType, mapper.NewBol<TBol>(record));

            return (TBol)SubBols[bolType];
        }

        public ChildBols<TBol> GetSubBols<TBol>(ICollection<TBol> bols) where TBol : IBOL, new()
        {
            if (bols == null)
                return null;

            Type bolType = typeof(TBol);

            if (!SubBols.ContainsKey(bolType))
                SubBols.Add(bolType, new ChildBols<TBol>(bols.ToList()));

            return (ChildBols<TBol>)SubBols[bolType];
        }

        public ChildBols<TInterface> GetSubBols<TInterface, TBol>(ICollection<IRecord> records)
            where TInterface: IBOL
            where TBol : TInterface, IBOL, new()
        {
            Type bolType = typeof(TBol);
            List<TInterface> interfaces = new List<TInterface>();

            if (records == null || !records.Any())
                SubBols.Add(bolType, new ChildBols<TInterface>(interfaces));

            if (!SubBols.ContainsKey(bolType))
            {
                List<TBol> bols = mapper.NewBols<TBol>(records.ToList());

                foreach(TBol bol in bols)
                    interfaces.Add((TInterface)bol);

                SubBols.Add(bolType, new ChildBols<TInterface>(interfaces));
            }
               
            return (ChildBols<TInterface>)SubBols[bolType];
        }

        public void FlagAsDeleted()
        {
            State = ObjectState.DETACHED;
        }
        #endregion

        protected virtual int GenerateID(ISequenced sequencedRecord)
        {
            return DBProviderHelper.GenerateID(sequencedRecord);
        }
    }
}
