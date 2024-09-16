using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.ViewModels;
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

        public Dictionary<string, IEnumerable<MasterDataViewModel>> GetAllMasterDataItems();

        public void CreateMasterDataItem(string name, string mdItemName, bool? isActive,bool isDesjardins );

        public void EditMasterDataItem(string mdName, string oldMdItemName, string newMdItemName, bool? isActive,bool isDesjardins);
        public void DeleteMasterDataItem(string mdName, string itemName);
        #endregion


        #region MdBank
        GetItemResponse<IMdBankBOL> GetMdBank(int id);
        GetListResponse<IMdBankBOL> GetAllMdBanks();
        public IMdBankBOL CreateBank(string name, bool? isActive, bool isDesjardins);
        public IMdBankBOL EditMdBank(string originalBankName, string newBankName, bool? isActive, bool isDesjardins);
        public int GetReferenceCountForMdBank(int bankId);
        public void DeleteMdBank(string itemName);



        #endregion

        #region MdEmploymentSituation
        GetItemResponse<IMdEmploymentSituationBOL> GetMdEmploymentSituation(int id);
        GetListResponse<IMdEmploymentSituationBOL> GetAllMdEmploymentSituations();
        public IMdEmploymentSituationBOL CreateMdEmploymentSituation(string name, bool? isActive);
        public IMdEmploymentSituationBOL EditMdEmploymentSituation(string oldMdEmploymentSituationName, string newMdEmploymentSituationName, bool? isActive);
        public int GetReferenceCountForMdEmploymentSituation(int id);
        public void DeleteMdEmploymentSituation(string itemName);
        #endregion

        #region MdMaritalStatus
        GetItemResponse<IMdMaritalStatusBOL> GetMdMaritalStatus(int id);
        GetListResponse<IMdMaritalStatusBOL> GetAllMdMaritalStatus();
        public IMdMaritalStatusBOL CreateMdMaritalStatus(string name, bool? isActive);
        public IMdMaritalStatusBOL EditMdMaritalStatus(string originalMdMaritalStatusName, string newMdMaritalStatusName, bool? isActive);
        public int GetReferenceCountForMdMaritalStatus(int id);
        public void DeleteMdMaritalStatus(string itemName);
        #endregion

        #region MdFamilySituation
        GetItemResponse<IMdFamilySituationBOL> GetMdFamilySituation(int id);
        GetListResponse<IMdFamilySituationBOL> GetAllMdFamilySituations();
        public IMdFamilySituationBOL CreateMdFamilySituation(string name, bool? isActive);
        public IMdFamilySituationBOL EditMdFamilySituation(string originalMdFamilySituationName, string newMdFamilySituationName, bool? isActive);
        public int GetReferenceCountForMdFamilySituation(int id);
        public void DeleteMdFamilySituation(string itemName);
        #endregion

        #region MdGenderDenomination
        GetItemResponse<IMdGenderDenominationBOL> GetMdGenderDenomination(int id);
        GetListResponse<IMdGenderDenominationBOL> GetAllMdGenderDenominations();
        public IMdGenderDenominationBOL CreateMdGenderDenomination(string name, bool? isActive);
        public IMdGenderDenominationBOL EditMdGenderDenomination(string originalMdGenderDenominationName, string newMdGenderDenominationName, bool? isActive);
        public int GetReferenceCountForMdGenderDenomination(int id);
        public void DeleteMdGenderDenomination(string itemName);
        #endregion

        #region MdHabitationType
        GetItemResponse<IMdHabitationTypeBOL> GetMdHabitationType(int id);
        GetListResponse<IMdHabitationTypeBOL> GetAllMdHabitationTypes();
        public IMdHabitationTypeBOL CreateMdHabitationType(string name, bool? isActive);
        public IMdHabitationTypeBOL EditMdHabitationType(string originalMdHabitationTypeName, string newMdHabitationTypeName, bool? isActive);
        public int GetReferenceCountForMdHabitationType(int id);
        public void DeleteMdHabitationType(string itemName);
        #endregion

        #region MdScholarshipType
        GetItemResponse<IMdScholarshipTypeBOL> GetMdScholarshipType(int id);
        GetListResponse<IMdScholarshipTypeBOL> GetAllMdScholarshipTypes();
        public IMdScholarshipTypeBOL CreateMdScholarshipType(string name, bool? isActive);
        public IMdScholarshipTypeBOL EditMdScholarshipType(string originalMdScholarshipTypeName, string newMdScholarshipTypeName, bool? isActive);
        public int GetReferenceCountForMdScholarshipType(int id);
        public void DeleteMdScholarshipType(string itemName);
        #endregion

        #region MdReferenceSource
        GetItemResponse<IMdReferenceSourceBOL> GetMdReferenceSource(int id);
        GetListResponse<IMdReferenceSourceBOL> GetAllMdReferenceSources();
        string GetReferenceTypeName(int referenceTypeId);
        public IMdReferenceSourceBOL CreateMdReferenceSource(string name, bool? isActive);
        public IMdReferenceSourceBOL EditMdReferenceSource(string originalMdReferenceSourceName, string newMdReferenceSourceName, bool? isActive);
        public int GetReferenceCountForMdReferenceSource(int id);
        public void DeleteMdReferenceSource(string itemName);
        #endregion

        #region MdInterventionStatusType
        GetItemResponse<IMdInterventionStatusTypeBOL> GetMdInterventionStatusType(int id);
        GetListResponse<IMdInterventionStatusTypeBOL> GetAllMdInterventionStatusTypes();
        string GetMdInterventionStatusTypeName(int statusId);
        public IMdInterventionStatusTypeBOL CreateMdInterventionStatusType(string name, bool? isActive);
        public IMdInterventionStatusTypeBOL EditMdInterventionStatusType(string originalMdInterventionStatusTypeName, string newMdInterventionStatusTypeName, bool? isActive);
        public int GetReferenceCountForMdInterventionStatusType(int id);
        public void DeleteMdInterventionStatusType(string itemName);
        #endregion

        #region MdLoanReason
        GetItemResponse<IMdLoanReasonBOL> GetMdLoanReason(int id);
        GetListResponse<IMdLoanReasonBOL> GetAllMdLoanReasons();
        string GetMdLoanReasonName(int loanReasonId);
        public IMdLoanReasonBOL CreateMdLoanReason(string name, bool? isActive);
        public IMdLoanReasonBOL EditMdLoanReason(string originalMdLoanReasonName, string newMdLoanReasonName, bool? isActive);
        public int GetReferenceCountForMdLoanReason(int id);
        public void DeleteMdLoanReason(string itemName);
        #endregion

        #region MdInterventionType
        GetItemResponse<IMdInterventionTypeBOL> GetMdInterventionType(int id);
        GetListResponse<IMdInterventionTypeBOL> GetAllMdInterventionTypes();
        string GetMdInterventionTypeName(int interventionTypeId);
        public IMdInterventionTypeBOL CreateMdInterventionType(string name, bool? isActive);
        public IMdInterventionTypeBOL EditMdInterventionType(string originalMdInterventionTypeName, string newMdInterventionTypeName, bool? isActive);
        public int GetReferenceCountForMdInterventionType(int id);
        public void DeleteMdInterventionType(string itemName);
        #endregion

        #region MdInterventionSolution
        GetItemResponse<IMdInterventionSolutionBOL> GetMdInterventionSolution(int id);
        GetListResponse<IMdInterventionSolutionBOL> GetAllMdInterventionSolutions();
        string GetSolutionName(int solutionId);
        public IMdInterventionSolutionBOL CreateMdInterventionSolution(string name, bool? isActive);
        public IMdInterventionSolutionBOL EditMdInterventionSolution(string originalMdInterventionSolutionName, string newMdInterventionSolutionName, bool? isActive);
        public int GetReferenceCountForMdInterventionSolution(int id);
        public void DeleteMdInterventionSolution(string itemName);
        #endregion

        #region MdIncomeType
        GetItemResponse<IMdIncomeTypeBOL> GetMdIncomeType(int id);
        GetListResponse<IMdIncomeTypeBOL> GetAllMdIncomeTypes();
        string GetIncomeTypeName(int incomeTypeId);
        public IMdIncomeTypeBOL CreateMdIncomeType(string name, bool? isActive);
        public IMdIncomeTypeBOL EditMdIncomeType(string originalMdIncomeTypeName, string newMdIncomeTypeName, bool? isActive);
        public int GetReferenceCountForMdIncomeType(int id);
        public void DeleteMdIncomeType(string itemName);
        #endregion

        #region MdSeminarTheme
        GetItemResponse<IMdSeminarThemesBOL> GetMdSeminarTheme(int id);
        GetListResponse<IMdSeminarThemesBOL> GetAllMdSeminarThemes();
        public IMdSeminarThemesBOL CreateMdSeminarTheme(string name, bool? isActive);
        public IMdSeminarThemesBOL EditMdSeminarTheme(string originalMdSeminarThemesName, string newMdSeminarThemesName, bool? isActive);
        public int GetReferenceCountForMdSeminarTheme(int id);
        public void DeleteMdSeminarTheme(string itemName);
        #endregion


    }
}
