using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Core.Definitions
{
    public abstract class AcefMockDAL
    {

        public int SaveEntity(IBOL item)
        {
            return 1;
        }

        public int SaveEntity(IBOL item, bool saveSubBols)
        {
            return 1;
        }

        public int SaveEntities(List<IBOL> items)
        {
            return 1;
        }

        public int AddEntity(IBOL newItem)
        {
            return 1;
        }

        public int AddEntities(List<IBOL> newItems)
        {
            return 1;
        }

        public int UpdateEntity(IBOL item)
        {
            return 1;
        }

        public int UpdateEntities(List<IBOL> items)
        {
            return 1;
        }

        public int DeleteEntity(IBOL item)
        {
            return 1;
        }

        public int DeleteEntity(IBOL item, bool immediateCommit)
        {
            return 1;
        }

        public int DeleteExtendedEntity(IExtendedBOL item)
        {
            return 1;
        }

        public int DeleteEntities(List<IBOL> items)
        {
            return 1;
        }

        public ITransaction BeginTransaction()
        {
            return null;
        }

        public ITransaction BeginTransaction(TransactionIsolationLevel transactionLevel)
        {
            return null;
        }

        public void Dispose()
        {

        }
    }
}
