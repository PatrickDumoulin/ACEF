using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Core.Definitions
{
    public interface IDbContextDAL<TDbContext> : IConnectionProvidedDAL
        where TDbContext : IDbContext
    {
        TDbContext Db { get; }

        List<IDbContextDAL<TDbContext>> GetChildTree();

        void InitializeConnection(IDbContextDAL<TDbContext> externalDal);

    }
}
