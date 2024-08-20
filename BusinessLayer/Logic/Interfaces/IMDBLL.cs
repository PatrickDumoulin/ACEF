using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataModels.BOL.IMdInterventionStatusType;
using DataModels.BOL.IMdScholarshipType;
using DataModels.BOL.MdBank;
using DataModels.BOL.MdEmploymentSituation;
using DataModels.BOL.MdFamilySituation;
using DataModels.BOL.MdGenderDenomination;
using DataModels.BOL.MdHabitationType;
using DataModels.BOL.MdIncomeType;
using DataModels.BOL.MdInterventionSolution;
using DataModels.BOL.MdInterventionType;
using DataModels.BOL.MdLoanReason;
using DataModels.BOL.MdMaritalStatus;
using DataModels.BOL.MdReferenceSource;
using DataModels.BOL.MdSeminarThemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
    public interface IMDBLL : IBLL
    {
        GetItemResponse<IMdBankBOL> GetMdBank(int id);
        GetListResponse<IMdBankBOL> GetAllMdBanks();

        GetItemResponse<IMdEmploymentSituationBOL> GetMdEmploymentSituation(int id);
        GetListResponse<IMdEmploymentSituationBOL> GetAllMdEmploymentSituations();

        GetItemResponse<IMdMaritalStatusBOL> GetMdMaritalStatus(int id);
        GetListResponse<IMdMaritalStatusBOL> GetAllMdMaritalStatus();

        GetItemResponse<IMdFamilySituationBOL> GetMdFamilySituation(int id);
        GetListResponse<IMdFamilySituationBOL> GetAllMdFamilySituations();

        GetItemResponse<IMdGenderDenominationBOL> GetMdGenderDenomination(int id);
        GetListResponse<IMdGenderDenominationBOL> GetAllMdGenderDenominations();

        GetItemResponse<IMdHabitationTypeBOL> GetMdHabitationType(int id);
        GetListResponse<IMdHabitationTypeBOL> GetAllMdHabitationTypes();

        GetItemResponse<IMdScholarshipTypeBOL> GetMdScholarshipType(int id);
        GetListResponse<IMdScholarshipTypeBOL> GetAllMdScholarshipTypes();

        GetItemResponse<IMdReferenceSourceBOL> GetMdReferenceSource(int id);
        GetListResponse<IMdReferenceSourceBOL> GetAllMdReferenceSources();
        string GetReferenceTypeName(int referenceTypeId);

        GetItemResponse<IMdInterventionStatusTypeBOL> GetMdInterventionStatusType(int id);
        GetListResponse<IMdInterventionStatusTypeBOL> GetAllMdInterventionStatusTypes();
        string GetMdInterventionStatusTypeName(int statusId);

        GetItemResponse<IMdLoanReasonBOL> GetMdLoanReason(int id);
        GetListResponse<IMdLoanReasonBOL> GetAllMdLoanReasons();

        GetItemResponse<IMdInterventionTypeBOL> GetMdInterventionType(int id);
        GetListResponse<IMdInterventionTypeBOL> GetAllMdInterventionTypes();
        string GetMdInterventionTypeName(int interventionTypeId);

        GetItemResponse<IMdInterventionSolutionBOL> GetMdInterventionSolution(int id);
        GetListResponse<IMdInterventionSolutionBOL> GetAllMdInterventionSolutions();

        string GetSolutionName(int solutionId);

        GetItemResponse<IMdIncomeTypeBOL> GetMdIncomeType(int id);
        GetListResponse<IMdIncomeTypeBOL> GetAllMdIncomeTypes();

        GetItemResponse<IMdSeminarThemesBOL> GetMdSeminarTheme(int id);
        GetListResponse<IMdSeminarThemesBOL> GetAllMdSeminarThemes();


    }
}
