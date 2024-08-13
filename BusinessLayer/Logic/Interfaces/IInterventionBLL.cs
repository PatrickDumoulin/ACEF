using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;


namespace BusinessLayer.Logic.Interfaces
{
    public interface IInterventionBLL : IBLL
    {
        GetItemResponse<InterventionBOL> GetIntervention(int id);

        GetListResponse<InterventionBOL> GetInterventions();

        void CreateIntervention(InterventionBOL interventionBOL);

        void UpdateIntervention(InterventionBOL interventionBOL);

        void DeleteIntervention(int id);
    }
}
