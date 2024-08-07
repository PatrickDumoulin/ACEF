using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Core.Configuration
{
    public static class ConfigurationHelper
    {
        public static void Initialize(IConfiguration Configuration)
        {
            Config = Configuration;
        }

        public static IConfiguration Config { get; private set; }
    }
}
