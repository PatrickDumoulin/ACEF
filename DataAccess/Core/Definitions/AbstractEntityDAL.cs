using CoreLib.Injection;
using CoreLib.Definitions;
using DataModels.Definitions;
using DataModels.Mappings;
using Microsoft.EntityFrameworkCore;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Core.Definitions
{
    public abstract class AbstractEntityDAL<TDbContext> : IDbContextDAL<TDbContext>
        where TDbContext : DbContext, IDbContext
    {
        #region private members
        private TDbContext db;
        private bool isDbContextExternal;
        protected EntityDALMappingFactoryWrapper MapperWrapper { get; private set; }
        private List<IDbContextDAL<TDbContext>> childList;
        private bool disposedValue = false; // To detect redundant calls
        private ITransaction internalScope = null;
        #endregion

        #region constructors

        public AbstractEntityDAL()
        {
            db = null;
            isDbContextExternal = false;
            MapperWrapper = new EntityDALMappingFactoryWrapper();
            UniqueGuid = Guid.NewGuid().ToString();

        }
        public AbstractEntityDAL(AbstractEntityDAL<TDbContext> externalDal)
        {
            this.db = externalDal.Db;
            isDbContextExternal = true;
            MapperWrapper = new EntityDALMappingFactoryWrapper();
            UniqueGuid = externalDal.UniqueGuid;
        }
        #endregion

        #region public/protected properties
        public TDbContext Db
        {
            get
            {
                if (!isDbContextExternal && db == null)
                {
                    db = GetDbInstance();
                }

                return db;
            }
        }

        public string UniqueGuid { get; private set; }
        #endregion

        #region public/protected methods
        public int AddEntity(IBOL newItem)
        {
            object newRecord = newItem.UntypedRecord;
            if (newRecord is ICreatedTimeStampedRecord)
                ((ICreatedTimeStampedRecord)newRecord).CreatedDate = DateTime.Now;

            if (newRecord is ILastModifiedTimeStampedRecord)
                ((ILastModifiedTimeStampedRecord)newRecord).LastModifiedDate = DateTime.Now;
            
            Type recordType = newRecord.GetType();
            Db.Add(newRecord);
            return Db.SaveChanges();
        }
        public int AddEntities(List<IBOL> newItems)
        {
            if (newItems.Count > 0)
            {
                List<IRecord> newRecords = newItems.Select(x => x.UntypedRecord).ToList();

                if (newRecords.Any(x => x is ICreatedTimeStampedRecord))
                    newRecords.ForEach(x => { ((ICreatedTimeStampedRecord)x).CreatedDate = DateTime.Now;});

                if (newRecords.Any(x => x is ILastModifiedTimeStampedRecord))
                    newRecords.ForEach(x => { ((ILastModifiedTimeStampedRecord)x).LastModifiedDate = DateTime.Now;});

                Type recordType = newRecords[0].GetType();
                Db.AddRange(newRecords);
                return Db.SaveChanges();
            }
            return 0;
        }

        public int UpdateEntity(IBOL item)
        {
            IRecord record = item.UntypedRecord;

            if (record is ILastModifiedTimeStampedRecord)
                ((ILastModifiedTimeStampedRecord)record).LastModifiedDate = DateTime.Now;
            
            foreach (IRecord child in record.LoadedRecords)
            {
                if (child is ICreatedTimeStampedRecord)
                {
                    if (Db.Entry(child).State == EntityState.Added)
                        ((ICreatedTimeStampedRecord)child).CreatedDate = DateTime.Now;                     
                }

                if (child is ILastModifiedTimeStampedRecord)              
                    ((ILastModifiedTimeStampedRecord)child).LastModifiedDate = DateTime.Now;                  
            }

            Db.Entry(record).State = EntityState.Modified;
            return Db.SaveChanges();
        }
        public int UpdateEntities(List<IBOL> items)
        {
            List<IRecord> records = items.Select(x => x.UntypedRecord).ToList();

            if (records.Any(x => x is ILastModifiedTimeStampedRecord))
                records.ForEach(x => { ((ILastModifiedTimeStampedRecord)x).LastModifiedDate = DateTime.Now;});
            
            records.ForEach(x => Db.Entry(x).State = EntityState.Modified);
            if (items.Any(x => x is IExtendedBOL))
                items.ForEach(x => ((IExtendedBOL)x).ExtendedRecordList.ForEach(y => Db.Entry(y).State = EntityState.Modified));

            int result = Db.SaveChanges();
            return result;
        }

        public int DeleteEntity(IBOL item)
        {
            return DeleteEntity(item, true);
        }

        public int DeleteEntity(IBOL item, bool immediateCommit)
        {
            BeforeDeleteEntity(item);

            var extendedBOL = item as IExtendedBOL;
            if (extendedBOL != null)
            {
                var extendedRecordList = extendedBOL.ExtendedRecordList;
                foreach (var extendedRecord in extendedRecordList)
                {
                    var recordState = Db.Entry(extendedRecord).State;
                    if (recordState != EntityState.Added)
                    {
                        Db.Entry(extendedRecord).State = EntityState.Deleted;
                    }
                }
                extendedBOL.MarkExtendedRecordAsDeleted();
            }
            object record = item.UntypedRecord;
            Db.Entry(record).State = EntityState.Deleted;

            if (immediateCommit)
                return Db.SaveChanges();
            else
                return 0;
        }

        public int DeleteExtendedEntity(IExtendedBOL item)
        {
            var extendedBOL = item as IExtendedBOL;
            var extendedRecordList = extendedBOL.ExtendedRecordList;
            foreach (var extendedRecord in extendedRecordList)
            {
                var recordState = Db.Entry(extendedRecord).State;
                if (recordState != EntityState.Added)
                    Db.Entry(extendedRecord).State = EntityState.Deleted;
            }
            return Db.SaveChanges();
        }

        public int DeleteEntities(List<IBOL> items)
        {
            int rowsDeleted = 0;
            items.ForEach(bolItem =>
            {
                BeforeDeleteEntity(bolItem);
                IExtendedBOL extendedBOL = bolItem as IExtendedBOL;
                if (extendedBOL != null)
                {
                    var extendedRecordList = extendedBOL.ExtendedRecordList;
                    foreach (var extendedRecord in extendedRecordList)
                    {
                        var recordState = Db.Entry(extendedRecord).State;
                        if (recordState != EntityState.Added)
                        {
                            Db.Entry(extendedRecord).State = EntityState.Deleted;
                        }
                    }
                    extendedBOL.MarkExtendedRecordAsDeleted();
                }
                IRecord record = bolItem.UntypedRecord;
                Db.Entry(record).State = EntityState.Deleted;
            });
            rowsDeleted += Db.SaveChanges();
            return rowsDeleted;
        }
        public int DetachEntities(List<IBOL> items)
        {
            int rowsDetached = 0;
            items.ForEach(bolItem =>
            {
                IRecord record = bolItem.UntypedRecord;
                Db.Entry(record).State = EntityState.Detached;
            });
            rowsDetached += Db.SaveChanges();
            return rowsDetached;
        }

        public int SaveEntities(List<IBOL> items)
        {
            int result = 0;

            if (items.Any(x => x.State == ObjectState.ADDED))
                result += AddEntities(items.Where(x => x.State == ObjectState.ADDED).ToList());
            if (items.Any(x => x.State == ObjectState.MODIFIED))
                result += UpdateEntities(items.Where(x => x.State == ObjectState.MODIFIED).ToList());
            if (items.Any(x => x.State == ObjectState.DELETED))
                result += DeleteEntities(items.Where(x => x.State == ObjectState.DELETED).ToList());
            if (items.Any(x => x.State == ObjectState.LOADED))
                result += items.Count(x => x.State == ObjectState.LOADED);
            if (items.Any(x => x.State == ObjectState.DETACHED))
                result += DetachEntities(items.Where(x => x.State == ObjectState.DETACHED).ToList());

            return result;
        }

        public int SaveEntity(IBOL item)
        {
            return SaveEntity(item, false);
        }

        public int SaveEntity(IBOL item, bool saveSubBols)
        {
            if (internalScope == null)
            {
                int result = 0;

                #region Resolve Transaction Level
                TransactionIsolationLevel level = TransactionIsolationLevel.ReadCommitted;

                if (System.Transactions.Transaction.Current != null)
                {
                    int tl = (int)System.Transactions.Transaction.Current.IsolationLevel;
                    level = (TransactionIsolationLevel)tl;
                }
                #endregion

                using (internalScope = BeginTransaction(level))
                {
                    result += internalSaveEntity(item, saveSubBols);
                    internalScope.Complete();
                }
                internalScope = null;
                return result;
            }
            else
            {
                return internalSaveEntity(item, saveSubBols);
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        public ITransaction BeginTransaction()
        {
            return BeginTransaction(TransactionIsolationLevel.ReadCommitted);
        }
        public ITransaction BeginTransaction(TransactionIsolationLevel transactionLevel)
        {
            return new Transaction(transactionLevel);
        }

        public void InitializeConnection(IDbContextDAL<TDbContext> externalDal)
        {
            if (externalDal is AbstractEntityDAL<TDbContext>)
            {
                AbstractEntityDAL<TDbContext> externalEntityDal = (AbstractEntityDAL<TDbContext>)externalDal;
                this.db = externalEntityDal.Db;
                isDbContextExternal = true;
                OnInitializeConnection(externalEntityDal);
            }
            else
            {
                throw new Exception("The dal object is not from the same ancestor as the calling Dal");
            }
        }
        public List<IDbContextDAL<TDbContext>> GetChildTree()
        {
            List<IDbContextDAL<TDbContext>> treeList = new List<IDbContextDAL<TDbContext>>();
            if (childList != null)
            {
                foreach (var child in childList)
                {
                    var childTree = child.GetChildTree();
                    childTree.ForEach(f =>
                    {
                        if (!treeList.Any(x => x.Equals(f)))
                        {
                            treeList.Add(f);
                        }
                    });

                    if (!treeList.Any(x => x.Equals(child)))
                    {
                        treeList.Add(child);
                    }
                }
            }

            return treeList;
        }

        protected virtual void BeforeDeleteEntity(IBOL bolItem) { }
        protected virtual TDbContext GetDbInstance()
        {
            IEntityConnectionProvider provider = Injector.ImplementItem<IEntityConnectionProvider>();
            Type contextType = typeof(TDbContext);
            var constructor = contextType.GetConstructor(new Type[] { typeof(string) });
            string connectionStringInstance = DBHelper.ResolveConnectionString(provider.ServerName, provider.DatabaseName);
            var instance = constructor.Invoke(new object[] { connectionStringInstance });
            var dbContext = (TDbContext)instance;
            dbContext.OnSaveException += OnSaveException;
            return dbContext;
        }

        protected abstract DatabaseTypes DatabaseType { get; }

        public string ConnectionString
        {
            get
            {
                return Db.Database.GetConnectionString();
            }
        }

        protected virtual void OnInitializeConnection(AbstractEntityDAL<TDbContext> externalEntityDal) { }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!isDbContextExternal && Db != null)
                    {
                        Db.Dispose();
                    }
                }

                disposedValue = true;
            }
        }
        #endregion

        #region private methods

        private int internalSaveEntity(IBOL item, bool saveSubBols)
        {
            int result = 0;

            if (saveSubBols && (item.State == ObjectState.DELETED || item.State == ObjectState.DETACHED))
            {
                foreach (KeyValuePair<Type, object> childBolItem in item.SubBols)
                {
                    IBOLCollection bolCollection = (IBOLCollection)childBolItem.Value;
                    var bolList = bolCollection.GetBolList();
                    foreach (var bolItem in bolList)
                    {
                        result += SaveEntity(bolItem, saveSubBols);
                    }
                }
            }

            switch (item.State)
            {
                case ObjectState.ADDED:
                    result += AddEntity(item);
                    break;
                case ObjectState.MODIFIED:
                    result += UpdateEntity(item);
                    break;
                case ObjectState.DELETED:
                    result += DeleteEntity(item);
                    break;
                case ObjectState.LOADED:
                    result += 1;
                    break;
                case ObjectState.DETACHED:
                    result += DetachEntities(new List<IBOL>() { item });
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (saveSubBols && (item.State != ObjectState.DELETED && item.State != ObjectState.DETACHED))
            {
                foreach (KeyValuePair<Type, object> childBolItem in item.SubBols)
                {
                    if (childBolItem.Value is IBOLCollection)
                    {
                        IBOLCollection bolCollection = (IBOLCollection)childBolItem.Value;
                        var bolList = bolCollection.GetBolList();
                        foreach (var bolItem in bolList)
                        {
                            result += SaveEntity(bolItem, saveSubBols);
                        }
                    }
                    else if (childBolItem.Value is IBOL)
                    {
                        IBOL childBolItemValue = (IBOL)childBolItem.Value;
                        result += SaveEntity(childBolItemValue, saveSubBols);
                    }
                }
            }

            return result;
        }

        private void OnSaveException(object sender, EventArgs e)
        {

            if (db != null)
                db.Dispose();

            db = null;

            db = GetDbInstance();

            List<IDbContextDAL<TDbContext>> treeList = GetChildTree();
            treeList.ForEach(f => f.InitializeConnection(this));
        }

        protected void addChildDal(IDbContextDAL<TDbContext> child)
        {
            if (childList == null)
                childList = new List<IDbContextDAL<TDbContext>>();

            childList.Add(child);
        }
        #endregion
    }
}
