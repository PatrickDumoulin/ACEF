using CoreLib.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Mappings
{
    public class LocalEnvironmentProvider : IEnvironmentProvider
    {
        public LocalEnvironmentProvider() { }

        public string Name { get { return "local";} }
    }
}
