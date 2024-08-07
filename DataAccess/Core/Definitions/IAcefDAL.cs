using CoreLib.Definitions;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Core.Definitions
{
    public interface IAcefDAL : IDbContextDAL<Projet_CLContext>
    {
        /*
        string DecryptFromFriendlyUrl(string data);
        string DecryptToken(byte[] value);
        string DecryptValue(byte[] value, string key);
        int DecryptId(string encryptedId);
        string EncryptToFriendlyUrl(string data);
        byte[] EncryptToken(string value);
        byte[] EncryptValue(string value, string key);
        byte[] GetByteArrayFromHexaString(string value);
        string GetHexaStringFromByteArray(byte[] value);
        byte[] HashValue(string value, string key);
        */

        /// <summary>
        /// Saves pending changes, if any
        /// </summary>
        void SavePendingChanges();

        int BulkSaveRecords(List<IBOL> bols);

        T GetDAL<T>() where T : IDAL;
        T GetDAL<T>(ProviderDALTypes dalType) where T : IDAL;
    }
}
