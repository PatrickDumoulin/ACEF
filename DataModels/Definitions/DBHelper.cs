using CoreLib.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Definitions
{
    public static class DBHelper
    {
        private static object sync_root = new object();

        private const string username = "sa";
        private const string password = "Cegep2024";

        public static string ResolveConnectionString(string serverName, string databaseName)
        {
            lock (sync_root)
            {
                try
                {
                    if (EnvironmentHelper.IsDebug())
                        return string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Encrypt=False;TrustServerCertificate=True;", serverName, databaseName);
                    else //Integrated for Prod if possible?
                        return string.Format(@"Data Source={0};Initial Catalog={1};user id={2};password={3};Encrypt=false;TrustServerCertificate=True", serverName, databaseName, username, password);
                }
                catch (Exception)
                {
                    return string.Format("Data Source={0};Initial Catalog={1};Integrated Security=False;Encrypt=False;TrustServerCertificate=True;user id={2};password={3}", serverName, databaseName, username, password);
                }
            }
        }

    }
}
