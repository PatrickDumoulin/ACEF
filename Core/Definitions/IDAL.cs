using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Definitions
{
    public interface IDAL : IDisposable
    {
        int SaveEntity(IBOL item);
        int SaveEntity(IBOL item, bool saveSubBols);
        int SaveEntities(List<IBOL> items);
        int AddEntity(IBOL newItem);
        int AddEntities(List<IBOL> newItems);
        int UpdateEntity(IBOL item);
        int UpdateEntities(List<IBOL> items);
        int DeleteEntity(IBOL item);
        int DeleteEntity(IBOL item, bool immediateCommit);

        int DeleteExtendedEntity(IExtendedBOL item);
        int DeleteEntities(List<IBOL> items);

        /// <summary>
        /// Starts a database transaction
        /// </summary>
        ITransaction BeginTransaction();

        /// <summary>
        /// Starts a database transaction with a specified isolation level
        /// </summary>
        /// <param name="transactionLevel"></param>
        /// <returns></returns>
        ITransaction BeginTransaction(TransactionIsolationLevel transactionLevel);
    }
}
