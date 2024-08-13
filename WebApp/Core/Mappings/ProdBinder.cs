using BusinessLayer.Core.Mappings;
using CoreLib.Configuration;
using DataModels.Mappings;

namespace WebApp.Core.Mappings
{
    public class ProdBinder : BusinessDependentBinder
    {
        public override void Load()
        {
            BindItem<IEntityConnectionProvider, AcefEntityConnectionProvider>();
            //BindItem<IEnvironmentProvider, LocalEnvironmentProvider>();

            BindItem<IEnvironmentProvider, ProdEnvironmentProvider>();
        }
    }
}
