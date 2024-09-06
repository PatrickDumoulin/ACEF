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
        #region MDGESTION
        public Dictionary<string, string> GetAllMDNames();

        public List<string> GetAllMdNamesByName(string name);

        public void CreateMasterDataItem(string name, string mdItemName, bool isActive);

        #endregion


        #region MdBank
        GetItemResponse<IMdBankBOL> GetMdBank(int id);
        GetListResponse<IMdBankBOL> GetAllMdBanks();
        public IMdBankBOL CreateBank(string name, bool isActive);

        #endregion

        #region MdEmploymentSituation
        GetItemResponse<IMdEmploymentSituationBOL> GetMdEmploymentSituation(int id);
        GetListResponse<IMdEmploymentSituationBOL> GetAllMdEmploymentSituations();
        public IMdEmploymentSituationBOL CreateMdEmploymentSituation(string name, bool isActive);
        #endregion

        #region MdMaritalStatus
        GetItemResponse<IMdMaritalStatusBOL> GetMdMaritalStatus(int id);
        GetListResponse<IMdMaritalStatusBOL> GetAllMdMaritalStatus();
        public IMdMaritalStatusBOL CreateMdMaritalStatus(string name, bool isActive);
        #endregion

        #region MdFamilySituation
        GetItemResponse<IMdFamilySituationBOL> GetMdFamilySituation(int id);
        GetListResponse<IMdFamilySituationBOL> GetAllMdFamilySituations();
        public IMdFamilySituationBOL CreateMdFamilySituation(string name, bool isActive);
        #endregion

        #region MdGenderDenomination
        GetItemResponse<IMdGenderDenominationBOL> GetMdGenderDenomination(int id);
        GetListResponse<IMdGenderDenominationBOL> GetAllMdGenderDenominations();
        public IMdGenderDenominationBOL CreateMdGenderDenomination(string name, bool isActive);
        #endregion

        #region MdHabitationType
        GetItemResponse<IMdHabitationTypeBOL> GetMdHabitationType(int id);
        GetListResponse<IMdHabitationTypeBOL> GetAllMdHabitationTypes();
        public IMdHabitationTypeBOL CreateMdHabitationType(string name, bool isActive);
        #endregion

        #region MdScholarshipType
        GetItemResponse<IMdScholarshipTypeBOL> GetMdScholarshipType(int id);
        GetListResponse<IMdScholarshipTypeBOL> GetAllMdScholarshipTypes();
        public IMdScholarshipTypeBOL CreateMdScholarshipType(string name, bool isActive);
        #endregion

        #region MdReferenceSource
        GetItemResponse<IMdReferenceSourceBOL> GetMdReferenceSource(int id);
        GetListResponse<IMdReferenceSourceBOL> GetAllMdReferenceSources();
        string GetReferenceTypeName(int referenceTypeId);
        public IMdReferenceSourceBOL CreateMdReferenceSource(string name, bool isActive);
        #endregion

        #region MdInterventionStatusType
        GetItemResponse<IMdInterventionStatusTypeBOL> GetMdInterventionStatusType(int id);
        GetListResponse<IMdInterventionStatusTypeBOL> GetAllMdInterventionStatusTypes();
        string GetMdInterventionStatusTypeName(int statusId);
        public IMdInterventionStatusTypeBOL CreateMdInterventionStatusType(string name, bool isActive);
        #endregion

        #region MdLoanReason
        GetItemResponse<IMdLoanReasonBOL> GetMdLoanReason(int id);
        GetListResponse<IMdLoanReasonBOL> GetAllMdLoanReasons();
        string GetMdLoanReasonName(int loanReasonId);
        public IMdLoanReasonBOL CreateMdLoanReason(string name, bool isActive);
        #endregion

        #region MdInterventionType
        GetItemResponse<IMdInterventionTypeBOL> GetMdInterventionType(int id);
        GetListResponse<IMdInterventionTypeBOL> GetAllMdInterventionTypes();
        string GetMdInterventionTypeName(int interventionTypeId);
        public IMdInterventionTypeBOL CreateMdInterventionType(string name, bool isActive);
        #endregion

        #region MdInterventionSolution
        GetItemResponse<IMdInterventionSolutionBOL> GetMdInterventionSolution(int id);
        GetListResponse<IMdInterventionSolutionBOL> GetAllMdInterventionSolutions();
        string GetSolutionName(int solutionId);
        public IMdInterventionSolutionBOL CreateMdInterventionSolution(string name, bool isActive);
        #endregion

        #region MdIncomeType
        GetItemResponse<IMdIncomeTypeBOL> GetMdIncomeType(int id);
        GetListResponse<IMdIncomeTypeBOL> GetAllMdIncomeTypes();
        public IMdIncomeTypeBOL CreateMdIncomeType(string name, bool isActive);
        #endregion

        #region MdSeminarTheme
        GetItemResponse<IMdSeminarThemesBOL> GetMdSeminarTheme(int id);
        GetListResponse<IMdSeminarThemesBOL> GetAllMdSeminarThemes();
        public IMdSeminarThemesBOL CreateMdSeminarTheme(string name, bool isActive);
        #endregion


    }
}
