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

namespace DataAccess.Providers.Interfaces
{
    public interface IMDDAL : IDAL
    {
        #region MdBank
        IMdBankBOL GetMdBank(int id);
        List<IMdBankBOL> GetAllMdBanks();
        public IMdBankBOL CreateMdBank(string bankName, bool? isActive);
        public IMdBankBOL EditMdBank(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdBank(int bankId);
        public void DeleteMdBank(string itemName);
        #endregion

        #region MdEmploymentSituation
        IMdEmploymentSituationBOL GetMdEmploymentSituation(int id);
        List<IMdEmploymentSituationBOL> GetAllMdEmploymentSituations();
        public IMdEmploymentSituationBOL CreateMdEmploymentSituation(string employmentSituation, bool? isActive);
        public IMdEmploymentSituationBOL EditMdEmploymentSituation(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdEmploymentSituation(int id);
        public void DeleteMdEmploymentSituation(string itemName);
        #endregion

        #region MdMaritalStatus
        IMdMaritalStatusBOL GetMdMaritalStatus(int id);
        List<IMdMaritalStatusBOL> GetAllMdMaritalStatus();
        public IMdMaritalStatusBOL CreateMdMaritalStatus(string mdMaritalStatus, bool? isActive);
        public IMdMaritalStatusBOL EditMdMaritalStatus(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdMaritalStatus(int id);
        public void DeleteMdMaritalStatus(string itemName);
        #endregion

        #region MdFamilySituation
        IMdFamilySituationBOL GetMdFamilySituation(int id);
        List<IMdFamilySituationBOL> GetAllMdFamilySituations();
        public IMdFamilySituationBOL CreateMdFamilySituation(string mdFamilySituation, bool? isActive);
        public IMdFamilySituationBOL EditMdFamilySituation(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdFamilySituation(int id);
        public void DeleteMdFamilySituation(string itemName);
        #endregion

        #region MdGenderDenomination
        IMdGenderDenominationBOL GetMdGenderDenomination(int id);
        List<IMdGenderDenominationBOL> GetAllMdGenderDenominations();
        public IMdGenderDenominationBOL CreateMdGenderDenomination(string mdGenderDenomination, bool? isActive);
        public IMdGenderDenominationBOL EditMdGenderDenomination(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdGenderDenomination(int id);
        public void DeleteMdGenderDenomination(string itemName);
        #endregion

        #region MdHabitationType
        IMdHabitationTypeBOL GetMdHabitationType(int id);
        List<IMdHabitationTypeBOL> GetAllMdHabitationTypes();
        public IMdHabitationTypeBOL CreateMdHabitationType(string mdHabitationType, bool? isActive);
        public IMdHabitationTypeBOL EditMdHabitationType(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdHabitationType(int id);
        public void DeleteMdHabitationType(string itemName);
        #endregion

        #region MdScholarshipType
        IMdScholarshipTypeBOL GetMdScholarshipType(int id);
        List<IMdScholarshipTypeBOL> GetAllMdScholarshipTypes();
        public IMdScholarshipTypeBOL CreateMdScholarshipType(string mdScholarshipType, bool? isActive);
        public IMdScholarshipTypeBOL EditMdScholarshipType(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdScholarshipType(int id);
        public void DeleteMdScholarshipType(string itemName);
        #endregion

        #region MdReferenceSource
        IMdReferenceSourceBOL GetMdReferenceSource(int id);
        List<IMdReferenceSourceBOL> GetAllMdReferenceSources();
        public IMdReferenceSourceBOL CreateMdReferenceSource(string mdReferenceSource, bool? isActive);
        public IMdReferenceSourceBOL EditMdReferenceSource(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdReferenceSource(int id);
        public void DeleteMdReferenceSource(string itemName);
        #endregion

        #region MdInterventionStatusType
        IMdInterventionStatusTypeBOL GetMdInterventionStatusType(int id);
        List<IMdInterventionStatusTypeBOL> GetAllMdInterventionStatusTypes();
        public IMdInterventionStatusTypeBOL CreateMdInterventionStatusType(string mdInterventionStatusType, bool? isActive);
        public IMdInterventionStatusTypeBOL EditMdInterventionStatusType(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdInterventionStatusType(int id);
        public void DeleteMdInterventionStatusType(string itemName);
        #endregion

        #region MdLoanReason
        IMdLoanReasonBOL GetMdLoanReason(int id);
        List<IMdLoanReasonBOL> GetAllMdLoanReasons();
        public IMdLoanReasonBOL CreateMdLoanReason(string mdLoanReason, bool? isActive);
        public IMdLoanReasonBOL EditMdLoanReason(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdLoanReason(int id);
        public void DeleteMdLoanReason(string itemName);
        #endregion

        #region MdInterventionType
        IMdInterventionTypeBOL GetMdInterventionType(int id);
        List<IMdInterventionTypeBOL> GetAllMdInterventionTypes();
        public IMdInterventionTypeBOL CreateMdInterventionType(string mdInterventionType, bool? isActive);
        public IMdInterventionTypeBOL EditMdInterventionType(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdInterventionType(int id);
        public void DeleteMdInterventionType(string itemName);
        #endregion

        #region MdInterventionSolution
        IMdInterventionSolutionBOL GetMdInterventionSolution(int id);
        List<IMdInterventionSolutionBOL> GetAllMdInterventionSolutions();
        public IMdInterventionSolutionBOL CreateMdInterventionSolution(string mdInterventionSolution, bool? isActive);
        public IMdInterventionSolutionBOL EditMdInterventionSolution(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdInterventionSolution(int id);
        public void DeleteMdInterventionSolution(string itemName);

        #endregion

        #region MdIncomeType
        IMdIncomeTypeBOL GetMdIncomeType(int id);
        List<IMdIncomeTypeBOL> GetAllMdIncomeTypes();
        public IMdIncomeTypeBOL CreateMdIncomeType(string mdIncomeType, bool? isActive);
        public IMdIncomeTypeBOL EditMdIncomeType(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdIncomeType(int id);
        public void DeleteMdIncomeType(string itemName);
        #endregion

        #region MdSeminarTheme
        IMdSeminarThemesBOL GetMdSeminarTheme(int id);
        List<IMdSeminarThemesBOL> GetAllMdSeminarThemes();
        public IMdSeminarThemesBOL CreateMdSeminarTheme(string mdSeminarTheme, bool? isActive);
        public IMdSeminarThemesBOL EditMdSeminarTheme(string oldMdItemName, string newMdItemName, bool? isActive);
        public int GetReferenceCountForMdSeminarTheme(int id);
        public void DeleteMdSeminarTheme(string itemName);
        #endregion




    }
}
