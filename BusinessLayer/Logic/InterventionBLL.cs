using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Communication.Responses.Interfaces;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.Providers.Interfaces;
using Ninject;
using System.Web.Mvc;

namespace BusinessLayer.Logic
{
    public class InterventionBLL : AbstractBLL<IInterventionDAL>, IInterventionBLL
    {
        
        public readonly IMDBLL _mdbBLL;
        public readonly IClientBLL _clientBLL;
        public readonly IEmployeeBLL _employeeBLL;
        public InterventionBLL() 
        { 
            
        }
        public InterventionBLL(ProviderDALTypes dalType) : base(dalType) { }
        public InterventionBLL(IDAL externalDAL) : base(externalDAL) { }

        public GetItemResponse<InterventionBOL> GetIntervention(int id)
        {
            var intervention = base.dal.GetIntervention(id);
            return new GetItemResponse<InterventionBOL>(intervention);
        }

        public GetListResponse<InterventionBOL> GetInterventions()
        {
            var interventions = base.dal.GetInterventions();
            return new GetListResponse<InterventionBOL>(interventions);
        }

        public void CreateIntervention(InterventionBOL interventionBOL)
        {
            base.dal.CreateIntervention(interventionBOL);
        }

        public void UpdateIntervention(InterventionBOL interventionBOL)
        {
            base.dal.UpdateIntervention(interventionBOL);
        }

        public void DeleteIntervention(int id)
        {
            base.dal.DeleteIntervention(id);
        }

        public DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;

        }

        public byte[] EncryptDebtAmount(decimal amount)
        {
            return BitConverter.GetBytes((double)amount);
        }

        public decimal DecryptDebtAmount(byte[] encryptedAmount)
        {
            return (decimal)BitConverter.ToDouble(encryptedAmount, 0);
        }




    }
}
