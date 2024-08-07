using DataModels.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Core.Configuration;

namespace UnitTests.Core.Mappings
{
    public class AcefEntityConnectionProvider : IEntityConnectionProvider
    {
        #region Constructor
        public AcefEntityConnectionProvider()
        {
            ServerName = ConfigurationHelper.Config["ServerName"];
            DatabaseName = ConfigurationHelper.Config["DatabaseName"];
        }
        #endregion

        #region Properties
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        #endregion
    }
}
