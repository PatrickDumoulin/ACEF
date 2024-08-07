using CoreLib.Definitions;
using CoreLib.Injection;
using DataAccess.Models;
using DataModels.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers
{
    public static class DBProviderHelper
    {
        private static CultureInfo numericEncryptedCulture;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static int GenerateID(ISequenced sequenced)
        {
            Projet_CLContext db = getDbInstance();
            Projet_CLContextProcedures procedures = new Projet_CLContextProcedures(db);

            return procedures.GenerateNewID(sequenced.SequenceName).First().NewID.Value;
        }
        
        public static CultureInfo NumericEncryptedCulture
        {
            get
            {
                setNumericEncryptedCulture();
                return numericEncryptedCulture;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void setNumericEncryptedCulture()
        {
            if (numericEncryptedCulture == null)
            {
                numericEncryptedCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                numericEncryptedCulture.NumberFormat.NumberGroupSeparator = "";
            }
        }

        public static byte[] EncryptValue(string data, IIdentifierEntity sequence)
        {
            Projet_CLContext db = getDbInstance();
            Projet_CLContextProcedures dbSP = new Projet_CLContextProcedures(db);

            var result = dbSP.EncryptData(data, sequence.Id.ToString(), new OutputParameter<byte[]>(), new OutputParameter<int>());
            return result[0].EncryptedValue; //stored proc can only return 1 value
        }

        public static string DecryptValue(byte[] encryptedData, IIdentifierEntity sequence)
        {
            return DecryptValue(encryptedData, sequence.Id);
        }

        public static string DecryptValue(byte[] encryptedData, int ID)
        {
            Projet_CLContext db = getDbInstance();
            Projet_CLContextProcedures dbSP = new Projet_CLContextProcedures(db);

            var result = dbSP.DecryptData(encryptedData, ID.ToString(), new OutputParameter<string>(), new OutputParameter<int>());
            return result[0].DecryptedValue; //stored proc can only return 1 value
        }

        private static Projet_CLContext getDbInstance()
        {
            IEntityConnectionProvider provider = Injector.ImplementItem<IEntityConnectionProvider>();
            string connectionString = DataModels.Definitions.DBHelper.ResolveConnectionString(provider.ServerName, provider.DatabaseName);

            var dbContext = new Projet_CLContext(connectionString);
            return dbContext;
        }
    }
}
