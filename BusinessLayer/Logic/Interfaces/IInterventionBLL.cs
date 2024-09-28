using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using System.Web.Mvc;



namespace BusinessLayer.Logic.Interfaces
{
    public interface IInterventionBLL : IBLL
    {
        GetItemResponse<InterventionBOL> GetIntervention(int id);

        GetListResponse<InterventionBOL> GetInterventions();

        void CreateIntervention(InterventionBOL interventionBOL);

        void UpdateIntervention(InterventionBOL interventionBOL);

        void DeleteIntervention(int id);

        DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek);

        byte[] EncryptDebtAmount(decimal amount);

        public decimal DecryptDebtAmount(byte[] encryptedAmount);

        public List<InterventionBOL> GetInterventionsByDateRange(DateTime startDate, DateTime endDate);










    }
}
