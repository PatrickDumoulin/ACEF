using CoreLib.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Mappings
{
    public class ProdEnvironmentProvider : IEnvironmentProvider
    {
        public ProdEnvironmentProvider() { }
        public string Name { get { return "prod"; } }
    }
}
