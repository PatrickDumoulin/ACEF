using DataModels.Mappings;
using WebApp.Core.Configuration;

namespace WebApp.Core.Mappings
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
