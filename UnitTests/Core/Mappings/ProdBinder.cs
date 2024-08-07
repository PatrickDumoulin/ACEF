using BusinessLayer.Core.Mappings;
using CoreLib.Configuration;
using DataModels.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Core.Mappings
{
    public class ProdBinder : BusinessDependentBinder
    {
        public override void Load()
        {
            BindItem<IEntityConnectionProvider, AcefEntityConnectionProvider>();
            BindItem<IEnvironmentProvider, LocalEnvironmentProvider>();
        }
    }
}
