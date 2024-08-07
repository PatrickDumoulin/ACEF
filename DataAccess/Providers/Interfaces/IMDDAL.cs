using CoreLib.Definitions;
using DataModels.BOL.IMdInterventionStatusType;
using DataModels.BOL.IMdScholarshipType;
using DataModels.BOL.MdBank;
using DataModels.BOL.MdEmploymentSituation;
using DataModels.BOL.MdFamilySituation;
using DataModels.BOL.MdGenderDenomination;
using DataModels.BOL.MdHabitationType;
using DataModels.BOL.MdInterventionSolution;
using DataModels.BOL.MdInterventionType;
using DataModels.BOL.MdLoanReason;
using DataModels.BOL.MdMaritalStatus;
using DataModels.BOL.MdReferenceSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface IMDDAL : IDAL
    {
        IMdBankBOL GetMdBank(int id);
        List<IMdBankBOL> GetAllMdBanks();

        IMdEmploymentSituationBOL GetMdEmploymentSituation(int id);
        List<IMdEmploymentSituationBOL> GetAllMdEmploymentSituations();

        IMdMaritalStatusBOL GetMdMaritalStatus(int id);
        List<IMdMaritalStatusBOL> GetAllMdMaritalStatus();

        IMdFamilySituationBOL GetMdFamilySituation(int id);
        List<IMdFamilySituationBOL> GetAllMdFamilySituations();

        IMdGenderDenominationBOL GetMdGenderDenomination(int id);
        List<IMdGenderDenominationBOL> GetAllMdGenderDenominations();

        IMdHabitationTypeBOL GetMdHabitationType(int id);
        List<IMdHabitationTypeBOL> GetAllMdHabitationTypes();

        IMdScholarshipTypeBOL GetMdScholarshipType(int id);
        List<IMdScholarshipTypeBOL> GetAllMdScholarshipTypes();

        IMdReferenceSourceBOL GetMdReferenceSource(int id);
        List<IMdReferenceSourceBOL> GetAllMdReferenceSources();

        IMdInterventionStatusTypeBOL GetMdInterventionStatusType(int id);
        List<IMdInterventionStatusTypeBOL> GetAllMdInterventionStatusTypes();

        IMdLoanReasonBOL GetMdLoanReason(int id);
        List<IMdLoanReasonBOL> GetAllMdLoanReasons();

        IMdInterventionTypeBOL GetMdInterventionType(int id);
        List<IMdInterventionTypeBOL> GetAllMdInterventionTypes();

        IMdInterventionSolutionBOL GetMdInterventionSolution(int id);
        List<IMdInterventionSolutionBOL> GetAllMdInterventionSolutions();
    }
}
