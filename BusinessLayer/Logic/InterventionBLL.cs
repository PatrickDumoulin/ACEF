using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.Intervention;
using DataAccess.Providers.Interfaces;

namespace BusinessLayer.Logic
{
    public class InterventionBLL : AbstractBLL<IInterventionDAL>, IInterventionBLL
    {
        public InterventionBLL() { }
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
    }
}
