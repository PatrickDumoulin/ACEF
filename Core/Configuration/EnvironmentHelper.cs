using CoreLib.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Configuration
{
    public static class EnvironmentHelper
    {
        private static string environmentName;

        public static bool IsDebug()
        {
            try
            {
                if (string.IsNullOrEmpty(environmentName))
                {
                    IEnvironmentProvider provider = Injector.ImplementItem<IEnvironmentProvider>();
                    environmentName = provider.Name.ToUpper();
                }

                return (environmentName == "LOCAL");
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
