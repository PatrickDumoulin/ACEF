using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.MdGenderDenomination;
using DataAccess.BOL.MdInterventionSolution;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
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
using System.Web.Mvc;

namespace BusinessLayer.Logic
{
    
    public class MDBLL: AbstractBLL<IMDDAL>, IMDBLL
    {
        public readonly IEmployeeBLL _employeeBLL;
        public MDBLL() { }
        public MDBLL(ProviderDALTypes dalType) : base(dalType) { }
        public MDBLL(IDAL externalDAL) : base(externalDAL) { }




        #region MDGESTION
        public Dictionary<string, string> GetAllMDNames()
        {
            return new Dictionary<string, string>
            {
                { nameof(MdBank), "Banque" },
                { nameof(MdEmploymentSituation), "Situation d'emploi" },
                { nameof(MdMaritalStatus), "État civil" },
                { nameof(MdFamilySituation), "Situation familiale" },
                { nameof(MdGenderDenomination), "Genre" },
                { nameof(MdHabitationType), "Type d'habitation" },
                { nameof(MdIncomeType), "Type de revenu" },
                { nameof(MdInterventionSolution), "Solution d'intervention" },
                { nameof(MdInterventionStatusTypes), "Type de statut d'intervention" },
                { nameof(MdInterventionType), "Type d'intervention" },
                { nameof(MdLoanReason), "Raison de prêt" },
                { nameof(MdReferenceSource), "Source de référence" },
                { nameof(MdScholarshipType), "Type de bourse" },
                { nameof(MdSeminarThemes), "Thème de séminaire" }
            };
        }

        public List<string> GetAllMdNamesByName(string name)
        {
            var mappings = new Dictionary<string, Func<IEnumerable<string>>>
            {
                { "Banque", () => GetAllMdBanks().ElementList.Select(b => b.Name).ToList() },
                { "Situation d'emploi", () => GetAllMdEmploymentSituations().ElementList.Select(b => b.Name).ToList() },
                { "État civil", () => GetAllMdMaritalStatus().ElementList.Select(b => b.Name).ToList() },
                { "Situation familiale", () => GetAllMdFamilySituations().ElementList.Select(b => b.Name).ToList() },
                { "Genre", () => GetAllMdGenderDenominations().ElementList.Select(b => b.Name).ToList() },
                { "Type d'habitation", () => GetAllMdHabitationTypes().ElementList.Select(b => b.Name).ToList() },
                { "Type de revenu", () => GetAllMdIncomeTypes().ElementList.Select(b => b.Name).ToList() },
                { "Solution d'intervention", () => GetAllMdInterventionSolutions().ElementList.Select(b => b.Name).ToList() },
                { "Type d'intervention", () => GetAllMdInterventionTypes().ElementList.Select(b => b.Name).ToList() },
                { "Type de statut d'intervention", () => GetAllMdInterventionStatusTypes().ElementList.Select(b => b.Name).ToList() },
                { "Raison de prêt", () => GetAllMdLoanReasons().ElementList.Select(b => b.Name).ToList() },
                { "Source de référence", () => GetAllMdReferenceSources().ElementList.Select(b => b.Name).ToList() },
                { "Type de bourse", () => GetAllMdScholarshipTypes().ElementList.Select(b => b.Name).ToList() },
                { "Thème de séminaire", () => GetAllMdSeminarThemes().ElementList.Select(b => b.Name).ToList() }
            };

            if (mappings.ContainsKey(name))
            {
                return mappings[name]().ToList();
            }

            return new List<string>();
        }

        public void CreateMasterDataItem(string name, string mdItemName, bool isActive)
        {
            var mappings = new Dictionary<string, Action<string, bool>>
            {
                { "Banque", (itemName, active) => CreateBank(itemName, active) },
                { "Situation d'emploi", (itemName, active) => CreateMdEmploymentSituation(itemName, active) },
                { "Genre", (itemName, active) => CreateMdGenderDenomination(itemName, active) },
                { "État civil", (itemName, active) => CreateMdMaritalStatus(itemName, active) },
                { "Situation familiale", (itemName, active) => CreateMdFamilySituation(itemName, active) },
                { "Type d'habitation", (itemName, active) => CreateMdHabitationType(itemName, active) },
                { "Type de revenu", (itemName, active) => CreateMdIncomeType(itemName, active) },
                { "Solution d'intervention", (itemName, active) => CreateMdInterventionSolution(itemName, active) },
                { "Type d'intervention", (itemName, active) => CreateMdInterventionType(itemName, active) },
                { "Type de statut d'intervention", (itemName, active) => CreateMdInterventionStatusType(itemName, active) },
                { "Raison de prêt", (itemName, active) => CreateMdLoanReason(itemName, active) },
                { "Source de référence", (itemName, active) => CreateMdReferenceSource(itemName, active) },
                { "Type de bourse", (itemName, active) => CreateMdScholarshipType(itemName, active) },
                { "Thème de séminaire", (itemName, active) => CreateMdSeminarTheme(itemName, active) },
                
            };

            if (mappings.ContainsKey(name))
            {
                mappings[name](mdItemName, isActive);
            }
            else
            {
                throw new Exception("Type de MasterData inconnu.");
            }
        }


        #endregion

        #region MdBank
        public GetItemResponse<IMdBankBOL> GetMdBank(int id)
        {
            IMdBankBOL mdBankBOL = base.dal.GetMdBank(id);
            return new GetItemResponse<IMdBankBOL>(mdBankBOL);
        }

        public GetListResponse<IMdBankBOL> GetAllMdBanks()
        {
            List<IMdBankBOL> mdBanksBOL = base.dal.GetAllMdBanks();
            return new GetListResponse<IMdBankBOL>(mdBanksBOL);
        }

        public IMdBankBOL CreateBank(string name, bool isActive)
        {
            return base.dal.CreateMdBank(name, isActive);
        }
        #endregion

        #region MdEmploymentSituation
        public GetItemResponse<IMdEmploymentSituationBOL> GetMdEmploymentSituation(int id)
        {
            IMdEmploymentSituationBOL mdEmploymentSituationBOL = base.dal.GetMdEmploymentSituation(id);
            return new GetItemResponse<IMdEmploymentSituationBOL>(mdEmploymentSituationBOL);
        }

        public GetListResponse<IMdEmploymentSituationBOL> GetAllMdEmploymentSituations()
        {
            List<IMdEmploymentSituationBOL> mdEmploymentSituationsBOL = base.dal.
                GetAllMdEmploymentSituations();
            return new GetListResponse<IMdEmploymentSituationBOL>(mdEmploymentSituationsBOL);
        }

        public IMdEmploymentSituationBOL CreateMdEmploymentSituation(string name, bool isActive)
        {
            return base.dal.CreateMdEmploymentSituation(name, isActive);
        }
        #endregion

        #region MdMaritalStatus
        public GetItemResponse<IMdMaritalStatusBOL> GetMdMaritalStatus(int id)
        {
            IMdMaritalStatusBOL mdMaritalStatusBOL = base.dal.GetMdMaritalStatus(id);
            return new GetItemResponse<IMdMaritalStatusBOL>(mdMaritalStatusBOL);
        }

        public GetListResponse<IMdMaritalStatusBOL> GetAllMdMaritalStatus()
        {
            List<IMdMaritalStatusBOL> mdMaritalStatusBOL = base.dal.
                GetAllMdMaritalStatus();
            return new GetListResponse<IMdMaritalStatusBOL>(mdMaritalStatusBOL);
        }

        public IMdMaritalStatusBOL CreateMdMaritalStatus(string name, bool isActive)
        {
            return base.dal.CreateMdMaritalStatus(name, isActive);
        }
        #endregion

        #region MdFamilySituation
        public GetItemResponse<IMdFamilySituationBOL> GetMdFamilySituation(int id)
        {
            IMdFamilySituationBOL mdFamilySituationBOL = base.dal.GetMdFamilySituation(id);
            return new GetItemResponse<IMdFamilySituationBOL>(mdFamilySituationBOL);
        }

        public GetListResponse<IMdFamilySituationBOL> GetAllMdFamilySituations()
        {
            List<IMdFamilySituationBOL> mdFamilySituationBOL = base.dal.
                GetAllMdFamilySituations();
            return new GetListResponse<IMdFamilySituationBOL>(mdFamilySituationBOL);
        }

        public IMdFamilySituationBOL CreateMdFamilySituation(string name, bool isActive)
        {
            return base.dal.CreateMdFamilySituation(name, isActive);
        }
        #endregion

        #region MdGenderDenomination
        public GetItemResponse<IMdGenderDenominationBOL> GetMdGenderDenomination(int id)
        {
            IMdGenderDenominationBOL mdGenderDenominationBOL = base.dal.GetMdGenderDenomination(id);
            return new GetItemResponse<IMdGenderDenominationBOL>(mdGenderDenominationBOL);
        }

        public GetListResponse<IMdGenderDenominationBOL> GetAllMdGenderDenominations()
        {
            List<IMdGenderDenominationBOL> mdGenderDenominationBOL = base.dal.
                GetAllMdGenderDenominations();
            return new GetListResponse<IMdGenderDenominationBOL>(mdGenderDenominationBOL);
        }

        public IMdGenderDenominationBOL CreateMdGenderDenomination(string name, bool isActive)
        {
            return base.dal.CreateMdGenderDenomination(name, isActive);
        }
        #endregion

        #region MdHabitationType
        public GetItemResponse<IMdHabitationTypeBOL> GetMdHabitationType(int id)
        {
            IMdHabitationTypeBOL mdHabitationTypeBOL = base.dal.GetMdHabitationType(id);
            return new GetItemResponse<IMdHabitationTypeBOL>(mdHabitationTypeBOL);
        }

        public GetListResponse<IMdHabitationTypeBOL> GetAllMdHabitationTypes()
        {
            List<IMdHabitationTypeBOL> mdHabitationTypeBOL = base.dal.
                GetAllMdHabitationTypes();
            return new GetListResponse<IMdHabitationTypeBOL>(mdHabitationTypeBOL);
        }

        public IMdHabitationTypeBOL CreateMdHabitationType(string name, bool isActive)
        {
            return base.dal.CreateMdHabitationType(name, isActive);
        }
        #endregion

        #region MdScholarshipType
        public GetItemResponse<IMdScholarshipTypeBOL> GetMdScholarshipType(int id)
        {
            IMdScholarshipTypeBOL mdScholarshipTypeBOL = base.dal.GetMdScholarshipType(id);
            return new GetItemResponse<IMdScholarshipTypeBOL>(mdScholarshipTypeBOL);
        }

        public GetListResponse<IMdScholarshipTypeBOL> GetAllMdScholarshipTypes()
        {
            List<IMdScholarshipTypeBOL> mdScholarshipTypeBOL = base.dal.
                GetAllMdScholarshipTypes();
            return new GetListResponse<IMdScholarshipTypeBOL>(mdScholarshipTypeBOL);
        }

        public IMdScholarshipTypeBOL CreateMdScholarshipType(string name, bool isActive)
        {
            return base.dal.CreateMdScholarshipType(name, isActive);
        }
        #endregion

        #region MdReferenceSource
        public GetItemResponse<IMdReferenceSourceBOL> GetMdReferenceSource(int id)
        {
            IMdReferenceSourceBOL mdReferenceSourceBOL = base.dal.GetMdReferenceSource(id);
            return new GetItemResponse<IMdReferenceSourceBOL>(mdReferenceSourceBOL);
        }

        public GetListResponse<IMdReferenceSourceBOL> GetAllMdReferenceSources()
        {
            List<IMdReferenceSourceBOL> mdReferenceSourceBOL = base.dal.
                GetAllMdReferenceSources();
            return new GetListResponse<IMdReferenceSourceBOL>(mdReferenceSourceBOL);
        }

        public string GetReferenceTypeName(int referenceTypeId)
        {
            var referenceResponse = GetMdReferenceSource(referenceTypeId);
            return referenceResponse.Succeeded && referenceResponse.Element != null
                ? referenceResponse.Element.Name
                : "Inconnu";
        }

        public IMdReferenceSourceBOL CreateMdReferenceSource(string name, bool isActive)
        {
            return base.dal.CreateMdReferenceSource(name, isActive);
        }
        #endregion

        #region MdInterventionStatusType
        public GetItemResponse<IMdInterventionStatusTypeBOL> GetMdInterventionStatusType(int id)
        {
            IMdInterventionStatusTypeBOL mdInterventionStatusTypeBOL = base.dal.GetMdInterventionStatusType(id);
            return new GetItemResponse<IMdInterventionStatusTypeBOL>(mdInterventionStatusTypeBOL);
        }

        public GetListResponse<IMdInterventionStatusTypeBOL> GetAllMdInterventionStatusTypes()
        {
            List<IMdInterventionStatusTypeBOL> mdInterventionStatusTypeBOL = base.dal.
                GetAllMdInterventionStatusTypes();
            return new GetListResponse<IMdInterventionStatusTypeBOL>(mdInterventionStatusTypeBOL);
        }

        public string GetMdInterventionStatusTypeName(int statusId)
        {
            var statusResponse = GetMdInterventionStatusType(statusId);
            return statusResponse.Succeeded && statusResponse.Element != null
                ? statusResponse.Element.Name
                : "Inconnu";
        }

        public IMdInterventionStatusTypeBOL CreateMdInterventionStatusType(string name, bool isActive)
        {
            return base.dal.CreateMdInterventionStatusType(name, isActive);
        }
        #endregion

        #region MdLoanReason
        public GetItemResponse<IMdLoanReasonBOL> GetMdLoanReason(int id)
        {
            IMdLoanReasonBOL mdLoanReasonBOL = base.dal.GetMdLoanReason(id);
            return new GetItemResponse<IMdLoanReasonBOL>(mdLoanReasonBOL);
        }

        public GetListResponse<IMdLoanReasonBOL> GetAllMdLoanReasons()
        {
            List<IMdLoanReasonBOL> mdLoanReasonBOL = base.dal.
                GetAllMdLoanReasons();
            return new GetListResponse<IMdLoanReasonBOL>(mdLoanReasonBOL);
        }

        public string GetMdLoanReasonName(int loanReasonId)
        {
            var loanReasonResponse = GetMdLoanReason(loanReasonId);
            return loanReasonResponse.Succeeded && loanReasonResponse.Element != null
                ? loanReasonResponse.Element.Name
                : "Inconnu";
        }

        public IMdLoanReasonBOL CreateMdLoanReason(string name, bool isActive)
        {
            return base.dal.CreateMdLoanReason(name, isActive);
        }
        #endregion

        #region MdInterventionType
        public GetItemResponse<IMdInterventionTypeBOL> GetMdInterventionType(int id)
        {
            IMdInterventionTypeBOL mdInterventionTypeBOL = base.dal.GetMdInterventionType(id);
            return new GetItemResponse<IMdInterventionTypeBOL>(mdInterventionTypeBOL);
        }


        public GetListResponse<IMdInterventionTypeBOL> GetAllMdInterventionTypes()
        {
            List<IMdInterventionTypeBOL> mdInterventionTypeBOL = base.dal.
                GetAllMdInterventionTypes();
            return new GetListResponse<IMdInterventionTypeBOL>(mdInterventionTypeBOL);
        }

        public string GetMdInterventionTypeName(int interventionTypeId)
        {
            var interventionResponse = GetMdInterventionType(interventionTypeId);
            return interventionResponse.Succeeded && interventionResponse.Element != null
                ? interventionResponse.Element.Name
                : "Inconnu";
        }

        public IMdInterventionTypeBOL CreateMdInterventionType(string name, bool isActive)
        {
            return base.dal.CreateMdInterventionType(name, isActive);
        }
        #endregion

        #region MdInterventionSolution
        public GetItemResponse<IMdInterventionSolutionBOL> GetMdInterventionSolution(int id)
        {
            IMdInterventionSolutionBOL mdInterventionSolutionBOL = base.dal.GetMdInterventionSolution(id);
            return new GetItemResponse<IMdInterventionSolutionBOL>(mdInterventionSolutionBOL);
        }

        public GetListResponse<IMdInterventionSolutionBOL> GetAllMdInterventionSolutions()
        {
            List<IMdInterventionSolutionBOL> mdInterventionSolutionBOL = base.dal.
                GetAllMdInterventionSolutions();
            return new GetListResponse<IMdInterventionSolutionBOL>(mdInterventionSolutionBOL);
        }

        public string GetSolutionName(int solutionId)
        {
            var solutionResponse = GetMdInterventionSolution(solutionId);
            return solutionResponse.Succeeded && solutionResponse.Element != null
                ? solutionResponse.Element.Name
                : "Inconnu";
        }

        public IMdInterventionSolutionBOL CreateMdInterventionSolution(string name, bool isActive)
        {
            return base.dal.CreateMdInterventionSolution(name, isActive);
        }
        #endregion

        #region MdIncomeType
        public GetItemResponse<IMdIncomeTypeBOL> GetMdIncomeType(int id)
        {
            IMdIncomeTypeBOL mdIncomeTypeBOL = base.dal.GetMdIncomeType(id);
            return new GetItemResponse<IMdIncomeTypeBOL>(mdIncomeTypeBOL);
        }

        public GetListResponse<IMdIncomeTypeBOL> GetAllMdIncomeTypes()
        {
            List<IMdIncomeTypeBOL> mdIncomeTypeBOL = base.dal.GetAllMdIncomeTypes();
            return new GetListResponse<IMdIncomeTypeBOL>(mdIncomeTypeBOL);
        }

        public IMdIncomeTypeBOL CreateMdIncomeType(string name, bool isActive)
        {
            return base.dal.CreateMdIncomeType(name, isActive);
        }
        #endregion

        #region MdSeminarTheme
        public GetItemResponse<IMdSeminarThemesBOL> GetMdSeminarTheme(int id)
        {
            IMdSeminarThemesBOL mdSeminarThemeBOL = base.dal.GetMdSeminarTheme(id);
            return new GetItemResponse<IMdSeminarThemesBOL>(mdSeminarThemeBOL);
        }

        public GetListResponse<IMdSeminarThemesBOL> GetAllMdSeminarThemes()
        {
            List<IMdSeminarThemesBOL> mdSeminarThemeBOL = base.dal.GetAllMdSeminarThemes();
            return new GetListResponse<IMdSeminarThemesBOL>(mdSeminarThemeBOL);
        }

        public IMdSeminarThemesBOL CreateMdSeminarTheme(string name, bool isActive)
        {
            return base.dal.CreateMdSeminarTheme(name, isActive);
        }
        #endregion




    }
}
