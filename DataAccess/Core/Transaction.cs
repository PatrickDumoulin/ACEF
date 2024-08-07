using CoreLib.Definitions;
using DataAccess.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Core
{
    public class Transaction : ITransaction
    {
        private TransactionScope scope;

        public Transaction() : this(TransactionIsolationLevel.ReadCommitted) { }

        public Transaction(TransactionIsolationLevel level)
        {
            if (level == TransactionIsolationLevel.Unspecified)
                scope = new TransactionScope();
            else
            {
                var txOptions = new TransactionOptions();
                int tl = (int)level;
                txOptions.IsolationLevel = (IsolationLevel)tl;
                txOptions.Timeout = new TimeSpan(0, 10, 0);
                scope = new TransactionScope(TransactionScopeOption.Required, txOptions);
            }
        }

        public void Complete()
        {
            scope.Complete();
        }
        public void Dispose()
        {
            if (scope != null)
            {
                scope.Dispose();
                scope = null;
            }
        }
    }
}
