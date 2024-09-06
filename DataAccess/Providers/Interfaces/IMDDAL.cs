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
        public IMdBankBOL CreateMdBank(string bankName, bool isActive);
        #endregion

        #region MdEmploymentSituation
        IMdEmploymentSituationBOL GetMdEmploymentSituation(int id);
        List<IMdEmploymentSituationBOL> GetAllMdEmploymentSituations();
        #endregion

        #region MdMaritalStatus
        IMdMaritalStatusBOL GetMdMaritalStatus(int id);
        List<IMdMaritalStatusBOL> GetAllMdMaritalStatus();
        #endregion

        #region MdFamilySituation
        IMdFamilySituationBOL GetMdFamilySituation(int id);
        List<IMdFamilySituationBOL> GetAllMdFamilySituations();
        #endregion

        #region MdGenderDenomination
        IMdGenderDenominationBOL GetMdGenderDenomination(int id);
        List<IMdGenderDenominationBOL> GetAllMdGenderDenominations();
        #endregion

        #region MdHabitationType
        IMdHabitationTypeBOL GetMdHabitationType(int id);
        List<IMdHabitationTypeBOL> GetAllMdHabitationTypes();
        #endregion

        #region MdScholarshipType
        IMdScholarshipTypeBOL GetMdScholarshipType(int id);
        List<IMdScholarshipTypeBOL> GetAllMdScholarshipTypes();
        #endregion

        #region MdReferenceSource
        IMdReferenceSourceBOL GetMdReferenceSource(int id);
        List<IMdReferenceSourceBOL> GetAllMdReferenceSources();
        #endregion

        #region MdInterventionStatusType
        IMdInterventionStatusTypeBOL GetMdInterventionStatusType(int id);
        List<IMdInterventionStatusTypeBOL> GetAllMdInterventionStatusTypes();
        #endregion

        #region MdLoanReason
        IMdLoanReasonBOL GetMdLoanReason(int id);
        List<IMdLoanReasonBOL> GetAllMdLoanReasons();
        #endregion

        #region MdInterventionType
        IMdInterventionTypeBOL GetMdInterventionType(int id);
        List<IMdInterventionTypeBOL> GetAllMdInterventionTypes();
        #endregion

        #region MdInterventionSolution
        IMdInterventionSolutionBOL GetMdInterventionSolution(int id);
        List<IMdInterventionSolutionBOL> GetAllMdInterventionSolutions();
        #endregion

        #region MdIncomeType
        IMdIncomeTypeBOL GetMdIncomeType(int id);
        List<IMdIncomeTypeBOL> GetAllMdIncomeTypes();
        #endregion

        #region MdSeminarTheme
        IMdSeminarThemesBOL GetMdSeminarTheme(int id);
        List<IMdSeminarThemesBOL> GetAllMdSeminarThemes();
        #endregion




    }
}
