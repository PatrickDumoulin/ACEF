﻿using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using BusinessLayer.ViewModels;
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

        public Dictionary<string, IEnumerable<MasterDataViewModel>> GetAllMasterDataItems()
        {
            var masterDataDictionary = new Dictionary<string, IEnumerable<MasterDataViewModel>>
            {
                { "Banque", GetAllMdBanks().ElementList.Select(b => new MasterDataViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    IsActive = b.Active,
                    FrenchDisplayName = "Banque",
                    ReferredCount = GetReferenceCountForMdBank(b.Id) // Compter les références pour chaque banque
                })},

                { "Situation d'emploi", GetAllMdEmploymentSituations().ElementList.Select(b => new MasterDataViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    IsActive = b.Active,
                    FrenchDisplayName = "Situation d'emploi",
                    ReferredCount = GetReferenceCountForMdEmploymentSituation(b.Id) // Compter les références pour chaque banque
                })},

                { "État civil", GetAllMdMaritalStatus().ElementList.Select(ms => new MasterDataViewModel
                {
                    Id = ms.Id,
                    Name = ms.Name,
                    IsActive = ms.Active,
                    FrenchDisplayName = "État civil",
                    ReferredCount = GetReferenceCountForMdMaritalStatus(ms.Id) // Compter les références pour chaque banque
                })},

                { "Situation familiale", GetAllMdFamilySituations().ElementList.Select(fs => new MasterDataViewModel
                {
                    Id = fs.Id,
                    Name = fs.Name,
                    IsActive = fs.Active,
                    FrenchDisplayName = "Situation familiale",
                    ReferredCount = GetReferenceCountForMdFamilySituation(fs.Id) // Compter les références pour chaque banque
                })},

                { "Genre", GetAllMdGenderDenominations().ElementList.Select(gd => new MasterDataViewModel
                {
                    Id = gd.Id,
                    Name = gd.Name,
                    IsActive = gd.Active,
                    FrenchDisplayName = "Genre",
                    ReferredCount = GetReferenceCountForMdGenderDenomination(gd.Id) // Compter les références pour chaque banque
                })},

                { "Type d'habitation", GetAllMdHabitationTypes().ElementList.Select(ht => new MasterDataViewModel
                {
                    Id = ht.Id,
                    Name = ht.Name,
                    IsActive = ht.Active,
                    FrenchDisplayName = "Type d'habitation",
                    ReferredCount = GetReferenceCountForMdHabitationType(ht.Id) // Compter les références pour chaque banque
                })},

                { "Type de revenu", GetAllMdIncomeTypes().ElementList.Select(it => new MasterDataViewModel
                {
                    Id = it.Id,
                    Name = it.Name,
                    IsActive = it.Active,
                    FrenchDisplayName = "Type de revenu",
                    ReferredCount = GetReferenceCountForMdIncomeType(it.Id) // Compter les références pour chaque banque
                })},

                { "Solution d'intervention", GetAllMdInterventionSolutions().ElementList.Select(isol => new MasterDataViewModel
                {
                    Id = isol.Id,
                    Name = isol.Name,
                    IsActive = isol.Active,
                    FrenchDisplayName = "Solution d'intervention",
                    ReferredCount = GetReferenceCountForMdInterventionSolution(isol.Id) // Compter les références pour chaque banque
                })},

                { "Type d'intervention", GetAllMdInterventionTypes().ElementList.Select(it => new MasterDataViewModel
                {
                    Id = it.Id,
                    Name = it.Name,
                    IsActive = it.Active,
                    FrenchDisplayName = "Type d'intervention",
                    ReferredCount = GetReferenceCountForMdInterventionType(it.Id) // Compter les références pour chaque banque
                })},

                { "Type de statut d'intervention", GetAllMdInterventionStatusTypes().ElementList.Select(ist => new MasterDataViewModel
                {
                    Id = ist.Id,
                    Name = ist.Name,
                    IsActive = ist.Active,
                    FrenchDisplayName = "Type de statut d'intervention",
                    ReferredCount = GetReferenceCountForMdInterventionStatusType(ist.Id) // Compter les références pour chaque banque
                })},

                { "Raison de prêt", GetAllMdLoanReasons().ElementList.Select(lr => new MasterDataViewModel
                {
                    Id = lr.Id,
                    Name = lr.Name,
                    IsActive = lr.Active,
                    FrenchDisplayName = "Raison de prêt",
                    ReferredCount = GetReferenceCountForMdLoanReason(lr.Id) // Compter les références pour chaque banque
                })},

                { "Source de référence", GetAllMdReferenceSources().ElementList.Select(rs => new MasterDataViewModel
                {
                    Id = rs.Id,
                    Name = rs.Name,
                    IsActive = rs.Active,
                    FrenchDisplayName = "Source de référence",
                    ReferredCount = GetReferenceCountForMdReferenceSource(rs.Id) // Compter les références pour chaque banque
                })},

                { "Type de bourse", GetAllMdScholarshipTypes().ElementList.Select(st => new MasterDataViewModel
                {
                    Id = st.Id,
                    Name = st.Name,
                    IsActive = st.Active,
                    FrenchDisplayName = "Type de bourse",
                    ReferredCount = GetReferenceCountForMdScholarshipType(st.Id) // Compter les références pour chaque banque
                })},

                { "Thème de séminaire", GetAllMdSeminarThemes().ElementList.Select(st => new MasterDataViewModel
                {
                    Id = st.Id,
                    Name = st.Name,
                    IsActive = st.Active,
                    FrenchDisplayName = "Thème de séminaire",
                    ReferredCount = GetReferenceCountForMdSeminarTheme(st.Id) // Compter les références pour chaque banque
                })}
             };

            return masterDataDictionary;
        }

        public void CreateMasterDataItem(string name, string mdItemName, bool? isActive)
        {
            var mappings = new Dictionary<string, Action<string, bool?>>
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

        public void EditMasterDataItem(string mdName, string oldMdItemName, string newMdItemName, bool? isActive)
        {
            var mappings = new Dictionary<string, Action<string, bool?>>
            {
                { "Banque", (itemName, active) => EditMdBank(oldMdItemName, newMdItemName, isActive ) },
                { "Situation d'emploi", (itemName, active) => EditMdEmploymentSituation(oldMdItemName, newMdItemName, active) },
                { "Genre", (itemName, active) => EditMdGenderDenomination(oldMdItemName, newMdItemName, active) },
                { "État civil", (itemName, active) => EditMdMaritalStatus(oldMdItemName, newMdItemName, active) },
                { "Situation familiale", (itemName, active) => EditMdFamilySituation(oldMdItemName, newMdItemName, active) },
                { "Type d'habitation", (itemName, active) => EditMdHabitationType(oldMdItemName, newMdItemName, active) },
                { "Type de revenu", (itemName, active) => EditMdIncomeType(oldMdItemName, newMdItemName, active) },
                { "Solution d'intervention", (itemName, active) => EditMdInterventionSolution(oldMdItemName, newMdItemName, active) },
                { "Type d'intervention", (itemName, active) => EditMdInterventionType(oldMdItemName, newMdItemName, active) },
                { "Type de statut d'intervention", (itemName, active) => EditMdInterventionStatusType(oldMdItemName, newMdItemName, active) },
                { "Raison de prêt", (itemName, active) => EditMdLoanReason(oldMdItemName, newMdItemName, active) },
                { "Source de référence", (itemName, active) => EditMdReferenceSource(oldMdItemName, newMdItemName, active) },
                { "Type de bourse", (itemName, active) => EditMdScholarshipType(oldMdItemName, newMdItemName, active) },
                { "Thème de séminaire", (itemName, active) => EditMdSeminarTheme(oldMdItemName, newMdItemName, active) },

            };

            if (mappings.ContainsKey(mdName))
            {
                mappings[mdName](newMdItemName, isActive);
            }
            else
            {
                throw new Exception("Type de MasterData inconnu.");
            }
        }

        public void DeleteMasterDataItem(string mdName, string itemName)
        {
            var mappings = new Dictionary<string, Action<string>>
            {
                { "Banque", (name) => DeleteMdBank(name) },
                { "Situation d'emploi", (name) => DeleteMdEmploymentSituation(name) },
                { "Genre", (name) => DeleteMdGenderDenomination(name) },
                { "État civil", (name) => DeleteMdMaritalStatus(name) },
                { "Situation familiale", (name) => DeleteMdFamilySituation(name) },
                { "Type d'habitation", (name) => DeleteMdHabitationType(name) },
                { "Type de revenu", (name) => DeleteMdIncomeType(name) },
                { "Solution d'intervention", (name) => DeleteMdInterventionSolution(name) },
                { "Type d'intervention", (name) => DeleteMdInterventionType(name) },
                { "Type de statut d'intervention", (name) => DeleteMdInterventionStatusType(name) },
                { "Raison de prêt", (name) => DeleteMdLoanReason(name) },
                { "Source de référence", (name) => DeleteMdReferenceSource(name) },
                { "Type de bourse", (name) => DeleteMdScholarshipType(name) },
                { "Thème de séminaire", (name) => DeleteMdSeminarTheme(name) }
            };

            if (mappings.ContainsKey(mdName))
            {
                // Appel de la méthode de suppression spécifique
                mappings[mdName](itemName);
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

        public IMdBankBOL CreateBank(string name, bool? isActive)
        {
            return base.dal.CreateMdBank(name, isActive);
        }

        public IMdBankBOL EditMdBank(string oldBankName, string newBankName, bool? isActive)
        {
            return base.dal.EditMdBank(oldBankName, newBankName, isActive);
        }

        public int GetReferenceCountForMdBank(int bankId)
        {
            return base.dal.GetReferenceCountForMdBank(bankId);
        }

        public void DeleteMdBank(string itemName)
        {
           base.dal.DeleteMdBank(itemName);
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

        public IMdEmploymentSituationBOL CreateMdEmploymentSituation(string name, bool? isActive)
        {
            return base.dal.CreateMdEmploymentSituation(name, isActive);
        }

        public IMdEmploymentSituationBOL EditMdEmploymentSituation(string originalMdEmploymentSituationName, string newMdEmploymentSituationName, bool? isActive)
        {
            return base.dal.EditMdEmploymentSituation(originalMdEmploymentSituationName, newMdEmploymentSituationName, isActive);
        }

        public int GetReferenceCountForMdEmploymentSituation(int id)
        {
            return base.dal.GetReferenceCountForMdEmploymentSituation(id);
        }

        public void DeleteMdEmploymentSituation(string itemName)
        {
            base.dal.DeleteMdEmploymentSituation(itemName);
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

        public IMdMaritalStatusBOL CreateMdMaritalStatus(string name, bool? isActive)
        {
            return base.dal.CreateMdMaritalStatus(name, isActive);
        }

        public IMdMaritalStatusBOL EditMdMaritalStatus(string originalMdMaritalStatusName, string newMdMaritalStatusName, bool? isActive)
        {
            return base.dal.EditMdMaritalStatus(originalMdMaritalStatusName, newMdMaritalStatusName, isActive);
        }

        public int GetReferenceCountForMdMaritalStatus(int id)
        {
            return base.dal.GetReferenceCountForMdMaritalStatus(id);
        }

        public void DeleteMdMaritalStatus(string itemName)
        {
            base.dal.DeleteMdMaritalStatus(itemName);
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

        public IMdFamilySituationBOL CreateMdFamilySituation(string name, bool? isActive)
        {
            return base.dal.CreateMdFamilySituation(name, isActive);
        }

        public IMdFamilySituationBOL EditMdFamilySituation(string originalMdFamilySituationName, string newMdFamilySituationName, bool? isActive)
        {
            return base.dal.EditMdFamilySituation(originalMdFamilySituationName, newMdFamilySituationName, isActive);
        }

        public int GetReferenceCountForMdFamilySituation(int id)
        {
            return base.dal.GetReferenceCountForMdFamilySituation(id);
        }

        public void DeleteMdFamilySituation(string itemName)
        {
            base.dal.DeleteMdFamilySituation(itemName);
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

        public IMdGenderDenominationBOL CreateMdGenderDenomination(string name, bool? isActive)
        {
            return base.dal.CreateMdGenderDenomination(name, isActive);
        }

        public IMdGenderDenominationBOL EditMdGenderDenomination(string originalMdGenderDenominationName, string newMdGenderDenominationName, bool? isActive)
        {
            return base.dal.EditMdGenderDenomination(originalMdGenderDenominationName, newMdGenderDenominationName, isActive);
        }

        public int GetReferenceCountForMdGenderDenomination(int id)
        {
            return base.dal.GetReferenceCountForMdGenderDenomination(id);
        }

        public void DeleteMdGenderDenomination(string itemName)
        {
            base.dal.DeleteMdGenderDenomination(itemName);
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

        public IMdHabitationTypeBOL CreateMdHabitationType(string name, bool? isActive)
        {
            return base.dal.CreateMdHabitationType(name, isActive);
        }

        public IMdHabitationTypeBOL EditMdHabitationType(string originalMdHabitationTypeName, string newMdHabitationTypeName, bool? isActive)
        {
            return base.dal.EditMdHabitationType(originalMdHabitationTypeName, newMdHabitationTypeName, isActive);
        }

        public int GetReferenceCountForMdHabitationType(int id)
        {
            return base.dal.GetReferenceCountForMdHabitationType(id);
        }

        public void DeleteMdHabitationType(string itemName)
        {
            base.dal.DeleteMdHabitationType(itemName);
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

        public IMdScholarshipTypeBOL CreateMdScholarshipType(string name, bool? isActive)
        {
            return base.dal.CreateMdScholarshipType(name, isActive);
        }

        public IMdScholarshipTypeBOL EditMdScholarshipType(string originalMdScholarshipTypeName, string newMdScholarshipTypeName, bool? isActive)
        {
            return base.dal.EditMdScholarshipType(originalMdScholarshipTypeName, newMdScholarshipTypeName, isActive);
        }

        public int GetReferenceCountForMdScholarshipType(int id)
        {
            return base.dal.GetReferenceCountForMdScholarshipType(id);
        }

        public void DeleteMdScholarshipType(string itemName)
        {
            base.dal.DeleteMdScholarshipType(itemName);
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

        public IMdReferenceSourceBOL CreateMdReferenceSource(string name, bool? isActive)
        {
            return base.dal.CreateMdReferenceSource(name, isActive);
        }

        public IMdReferenceSourceBOL EditMdReferenceSource(string originalMdReferenceSourceName, string newMdReferenceSourceName, bool? isActive)
        {
            return base.dal.EditMdReferenceSource(originalMdReferenceSourceName, newMdReferenceSourceName, isActive);
        }

        public int GetReferenceCountForMdReferenceSource(int id)
        {
            return base.dal.GetReferenceCountForMdReferenceSource(id);
        }

        public void DeleteMdReferenceSource(string itemName)
        {
            base.dal.DeleteMdReferenceSource(itemName);
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

        public IMdInterventionStatusTypeBOL CreateMdInterventionStatusType(string name, bool? isActive)
        {
            return base.dal.CreateMdInterventionStatusType(name, isActive);
        }

        public IMdInterventionStatusTypeBOL EditMdInterventionStatusType(string originalMdInterventionStatusTypeName, string newMdInterventionStatusTypeName, bool? isActive)
        {
            return base.dal.EditMdInterventionStatusType(originalMdInterventionStatusTypeName, newMdInterventionStatusTypeName, isActive);
        }

        public int GetReferenceCountForMdInterventionStatusType(int id)
        {
            return base.dal.GetReferenceCountForMdInterventionStatusType(id);
        }

        public void DeleteMdInterventionStatusType(string itemName)
        {
            base.dal.DeleteMdInterventionStatusType(itemName);
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

        public IMdLoanReasonBOL CreateMdLoanReason(string name, bool? isActive)
        {
            return base.dal.CreateMdLoanReason(name, isActive);
        }

        public IMdLoanReasonBOL EditMdLoanReason(string originalMdLoanReasonName, string newMdLoanReasonName, bool? isActive)
        {
            return base.dal.EditMdLoanReason(originalMdLoanReasonName, newMdLoanReasonName, isActive);
        }

        public int GetReferenceCountForMdLoanReason(int id)
        {
            return base.dal.GetReferenceCountForMdLoanReason(id);
        }

        public void DeleteMdLoanReason(string itemName)
        {
            base.dal.DeleteMdLoanReason(itemName);
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

        public IMdInterventionTypeBOL CreateMdInterventionType(string name, bool? isActive)
        {
            return base.dal.CreateMdInterventionType(name, isActive);
        }

        public IMdInterventionTypeBOL EditMdInterventionType(string originalMdInterventionTypeName, string newMdInterventionTypeName, bool? isActive)
        {
            return base.dal.EditMdInterventionType(originalMdInterventionTypeName, newMdInterventionTypeName, isActive);
        }

        public int GetReferenceCountForMdInterventionType(int id)
        {
            return base.dal.GetReferenceCountForMdInterventionType(id);
        }

        public void DeleteMdInterventionType(string itemName)
        {
            base.dal.DeleteMdInterventionType(itemName);
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

        public IMdInterventionSolutionBOL CreateMdInterventionSolution(string name, bool? isActive)
        {
            return base.dal.CreateMdInterventionSolution(name, isActive);
        }

        public IMdInterventionSolutionBOL EditMdInterventionSolution(string originalMdInterventionSolutionName, string newMdInterventionSolutionName, bool? isActive)
        {
            return base.dal.EditMdInterventionSolution(originalMdInterventionSolutionName, newMdInterventionSolutionName, isActive);
        }

        public int GetReferenceCountForMdInterventionSolution(int id)
        {
            return base.dal.GetReferenceCountForMdInterventionSolution(id);
        }

        public void DeleteMdInterventionSolution(string itemName)
        {
            base.dal.DeleteMdInterventionSolution(itemName);
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

        public IMdIncomeTypeBOL CreateMdIncomeType(string name, bool? isActive)
        {
            return base.dal.CreateMdIncomeType(name, isActive);
        }

        public IMdIncomeTypeBOL EditMdIncomeType(string originalMdIncomeTypeName, string newMdIncomeTypeName, bool? isActive)
        {
            return base.dal.EditMdIncomeType(originalMdIncomeTypeName, newMdIncomeTypeName, isActive);
        }

        public int GetReferenceCountForMdIncomeType(int id)
        {
            return base.dal.GetReferenceCountForMdIncomeType(id);
        }

        public void DeleteMdIncomeType(string itemName)
        {
            base.dal.DeleteMdIncomeType(itemName);
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

        public IMdSeminarThemesBOL CreateMdSeminarTheme(string name, bool? isActive)
        {
            return base.dal.CreateMdSeminarTheme(name, isActive);
        }

        public IMdSeminarThemesBOL EditMdSeminarTheme(string originalMdSeminarThemesName, string newMdSeminarThemesName, bool? isActive)
        {
            return base.dal.EditMdSeminarTheme(originalMdSeminarThemesName, newMdSeminarThemesName, isActive);
        }

        public int GetReferenceCountForMdSeminarTheme(int id)
        {
            return base.dal.GetReferenceCountForMdSeminarTheme(id);
        }

        public void DeleteMdSeminarTheme(string itemName)
        {
            base.dal.DeleteMdSeminarTheme(itemName);
        }
        #endregion




    }
}
