using CoreLib.Definitions;
using DataAccess.BOL.MdBank;
using DataAccess.BOL.MdEmploymentSituation;
using DataAccess.BOL.MdFamilySituation;
using DataAccess.BOL.MdGenderDenomination;
using DataAccess.BOL.MdHabitationType;
using DataAccess.BOL.MdIncomeType;
using DataAccess.BOL.MdInterventionSolution;
using DataAccess.BOL.MdInterventionStatusType;
using DataAccess.BOL.MdInterventionType;
using DataAccess.BOL.MdLoanReason;
using DataAccess.BOL.MdMaritalStatus;
using DataAccess.BOL.MdReferenceSource;
using DataAccess.BOL.MdScholarshipType;
using DataAccess.BOL.MdSeminarThemes;
using DataAccess.Core.Definitions;
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

namespace DataAccess.Providers.Entity
{
    public class MDEntityDAL : AcefEntityDAL, IMDDAL
    {
        public MDEntityDAL() { }
        public MDEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        

        #region MdBank
        public IMdBankBOL GetMdBank(int id)
        {
            MdBank record = Db.MdBank.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdBankBOL>(record);
        }

        public List<IMdBankBOL> GetAllMdBanks()
        {
            List<IRecord> records = Db.MdBank.ToList<IRecord>();
            return MapperWrapper.NewBols<MdBankBOL>(records).ToList<IMdBankBOL>();
        }

        public IMdBankBOL CreateMdBank(string bankName, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newBank = new MdBank
            {
                Name = bankName,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdBank.Add(newBank);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdBankBOL>(newBank);
        }
        #endregion 

        #region MdEmploymentSituation
        public IMdEmploymentSituationBOL GetMdEmploymentSituation(int id)
        {
            MdEmploymentSituation record = Db.MdEmploymentSituation.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdEmploymentSituationBOL>(record);
        }

        public List<IMdEmploymentSituationBOL> GetAllMdEmploymentSituations()
        {
            List<IRecord> records = Db.MdEmploymentSituation.ToList<IRecord>();
            return MapperWrapper.NewBols<MdEmploymentSituationBOL>(records).ToList<IMdEmploymentSituationBOL>();
        }

        public IMdEmploymentSituationBOL CreateMdEmploymentSituation(string employmentSituation, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newEmploymentSituation= new MdEmploymentSituation
            {
                Name = employmentSituation,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdEmploymentSituation.Add(newEmploymentSituation);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdEmploymentSituationBOL>(newEmploymentSituation);
        }
        #endregion

        #region MdMaritalStatus
        public IMdMaritalStatusBOL GetMdMaritalStatus(int id)
        {
            MdMaritalStatus record = Db.MdMaritalStatus.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdMaritalStatusBOL>(record);
        }

        public List<IMdMaritalStatusBOL> GetAllMdMaritalStatus()
        {
            List<IRecord> records = Db.MdMaritalStatus.ToList<IRecord>();
            return MapperWrapper.NewBols<MdMaritalStatusBOL>(records).ToList<IMdMaritalStatusBOL>();
        }

        public IMdMaritalStatusBOL CreateMdMaritalStatus(string mdMaritalStatus, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdMaritalStatus = new MdMaritalStatus
            {
                Name = mdMaritalStatus,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdMaritalStatus.Add(newMdMaritalStatus);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdMaritalStatusBOL>(newMdMaritalStatus);
        }
        #endregion

        #region MdFamilySituation
        public IMdFamilySituationBOL GetMdFamilySituation(int id)
        {
            MdFamilySituation record = Db.MdFamilySituation.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdFamilySituationBOL>(record);
        }

        public List<IMdFamilySituationBOL> GetAllMdFamilySituations()
        {
            List<IRecord> records = Db.MdFamilySituation.ToList<IRecord>();
            return MapperWrapper.NewBols<MdFamilySituationBOL>(records).ToList<IMdFamilySituationBOL>();
        }

        public IMdFamilySituationBOL CreateMdFamilySituation(string mdFamilySituation, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdFamilySituation = new MdFamilySituation
            {
                Name = mdFamilySituation,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdFamilySituation.Add(newMdFamilySituation);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdFamilySituationBOL>(newMdFamilySituation);
        }
        #endregion

        #region MdGenderDenomination
        public IMdGenderDenominationBOL GetMdGenderDenomination(int id)
        {
            MdGenderDenomination record = Db.MdGenderDenomination.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdGenderDenominationBOL>(record);
        }

        public List<IMdGenderDenominationBOL> GetAllMdGenderDenominations()
        {
            List<IRecord> records = Db.MdGenderDenomination.ToList<IRecord>();
            return MapperWrapper.NewBols<MdGenderDenominationBOL>(records).ToList<IMdGenderDenominationBOL>();
        }

        public IMdGenderDenominationBOL CreateMdGenderDenomination(string mdGenderDenomination, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdGenderDenomination = new MdGenderDenomination
            {
                Name = mdGenderDenomination,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdGenderDenomination.Add(newMdGenderDenomination);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdGenderDenominationBOL>(newMdGenderDenomination);
        }
        #endregion

        #region MdHabitationType
        public IMdHabitationTypeBOL GetMdHabitationType(int id)
        {
            MdHabitationType record = Db.MdHabitationType.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdHabitationTypeBOL>(record);
        }

        public List<IMdHabitationTypeBOL> GetAllMdHabitationTypes()
        {
            List<IRecord> records = Db.MdHabitationType.ToList<IRecord>();
            return MapperWrapper.NewBols<MdHabitationTypeBOL>(records).ToList<IMdHabitationTypeBOL>();
        }

        public IMdHabitationTypeBOL CreateMdHabitationType(string mdHabitationType, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdHabitationType = new MdHabitationType
            {
                Name = mdHabitationType,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdHabitationType.Add(newMdHabitationType);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdHabitationTypeBOL>(newMdHabitationType);
        }
        #endregion

        #region MdScholarshipType
        public IMdScholarshipTypeBOL GetMdScholarshipType(int id)
        {
            MdScholarshipType record = Db.MdScholarshipType.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdScholarshipTypeBOL>(record);
        }

        public List<IMdScholarshipTypeBOL> GetAllMdScholarshipTypes()
        {
            List<IRecord> records = Db.MdScholarshipType.ToList<IRecord>();
            return MapperWrapper.NewBols<MdScholarshipTypeBOL>(records).ToList<IMdScholarshipTypeBOL>();
        }

        public IMdScholarshipTypeBOL CreateMdScholarshipType(string mdScholarshipType, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdScholarshipType = new MdScholarshipType
            {
                Name = mdScholarshipType,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdScholarshipType.Add(newMdScholarshipType);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdScholarshipTypeBOL>(newMdScholarshipType);
        }
        #endregion

        #region MdReferenceSource
        public IMdReferenceSourceBOL GetMdReferenceSource(int id)
        {
            MdReferenceSource record = Db.MdReferenceSource.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdReferenceSourceBOL>(record);
        }

        public List<IMdReferenceSourceBOL> GetAllMdReferenceSources()
        {
            List<IRecord> records = Db.MdReferenceSource.ToList<IRecord>();
            return MapperWrapper.NewBols<MdReferenceSourceBOL>(records).ToList<IMdReferenceSourceBOL>();
        }

        public IMdReferenceSourceBOL CreateMdReferenceSource(string mdReferenceSource, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdReferenceSource = new MdReferenceSource
            {
                Name = mdReferenceSource,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdReferenceSource.Add(newMdReferenceSource);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdReferenceSourceBOL>(newMdReferenceSource);
        }
        #endregion

        #region MdInterventionStatusType
        public IMdInterventionStatusTypeBOL GetMdInterventionStatusType(int id)
        {
            MdInterventionStatusTypes record = Db.MdInterventionStatusTypes.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdInterventionStatusTypeBOL>(record);
        }

        public List<IMdInterventionStatusTypeBOL> GetAllMdInterventionStatusTypes()
        {
            List<IRecord> records = Db.MdInterventionStatusTypes.ToList<IRecord>();
            return MapperWrapper.NewBols<MdInterventionStatusTypeBOL>(records).ToList<IMdInterventionStatusTypeBOL>();
        }

        public IMdInterventionStatusTypeBOL CreateMdInterventionStatusType(string mdInterventionStatusType, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdInterventionStatusType = new MdInterventionStatusTypes
            {
                Name = mdInterventionStatusType,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdInterventionStatusTypes.Add(newMdInterventionStatusType);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdInterventionStatusTypeBOL>(newMdInterventionStatusType);
        }
        #endregion

        #region MdLoanReason
        public IMdLoanReasonBOL GetMdLoanReason(int id)
        {
            MdLoanReason record = Db.MdLoanReason.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdLoanReasonBOL>(record);
        }

        public List<IMdLoanReasonBOL> GetAllMdLoanReasons()
        {
            List<IRecord> records = Db.MdLoanReason.ToList<IRecord>();
            return MapperWrapper.NewBols<MdLoanReasonBOL>(records).ToList<IMdLoanReasonBOL>();
        }

        public IMdLoanReasonBOL CreateMdLoanReason(string mdLoanReason, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdLoanReason = new MdLoanReason
            {
                Name = mdLoanReason,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdLoanReason.Add(newMdLoanReason);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdLoanReasonBOL>(newMdLoanReason);
        }
        #endregion

        #region MdInterventionType
        public IMdInterventionTypeBOL GetMdInterventionType(int id)
        {
            MdInterventionType record = Db.MdInterventionType.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdInterventionTypeBOL>(record);
        }

        public List<IMdInterventionTypeBOL> GetAllMdInterventionTypes()
        {
            List<IRecord> records = Db.MdInterventionType.ToList<IRecord>();
            return MapperWrapper.NewBols<MdInterventionTypeBOL>(records).ToList<IMdInterventionTypeBOL>();
        }

        public IMdInterventionTypeBOL CreateMdInterventionType(string mdInterventionType, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdInterventionType = new MdInterventionType
            {
                Name = mdInterventionType,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdInterventionType.Add(newMdInterventionType);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdInterventionTypeBOL>(newMdInterventionType);
        }
        #endregion

        #region MdInterventionSolution
        public IMdInterventionSolutionBOL GetMdInterventionSolution(int id)
        {
            MdInterventionSolution record = Db.MdInterventionSolution.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdInterventionSolutionBOL>(record);
        }

        public List<IMdInterventionSolutionBOL> GetAllMdInterventionSolutions()
        {
            List<IRecord> records = Db.MdInterventionSolution.ToList<IRecord>();
            return MapperWrapper.NewBols<MdInterventionSolutionBOL>(records).ToList<IMdInterventionSolutionBOL>();
        }

        public IMdInterventionSolutionBOL CreateMdInterventionSolution(string mdInterventionSolution, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdInterventionSolution = new MdInterventionSolution
            {
                Name = mdInterventionSolution,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdInterventionSolution.Add(newMdInterventionSolution);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdInterventionSolutionBOL>(newMdInterventionSolution);
        }
        #endregion

        #region MdIncomeType
        public IMdIncomeTypeBOL GetMdIncomeType(int id)
        {
            MdIncomeType record = Db.MdIncomeType.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdIncomeTypeBOL>(record);
        }

        public List<IMdIncomeTypeBOL> GetAllMdIncomeTypes()
        {
            List<IRecord> records = Db.MdIncomeType.ToList<IRecord>();
            return MapperWrapper.NewBols<MdIncomeTypeBOL>(records).ToList<IMdIncomeTypeBOL>();
        }

        public IMdIncomeTypeBOL CreateMdIncomeType(string mdIncomeType, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdIncomeType = new MdIncomeType
            {
                Name = mdIncomeType,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdIncomeType.Add(newMdIncomeType);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdIncomeTypeBOL>(newMdIncomeType);
        }
        #endregion

        #region MdSeminarTheme
        public IMdSeminarThemesBOL GetMdSeminarTheme(int id)
        {
            MdSeminarThemes record = Db.MdSeminarThemes.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<MdSeminarThemesBOL>(record);
        }

        public List<IMdSeminarThemesBOL> GetAllMdSeminarThemes()
        {
            List<IRecord> records = Db.MdSeminarThemes.ToList<IRecord>();
            return MapperWrapper.NewBols<MdSeminarThemesBOL>(records).ToList<IMdSeminarThemesBOL>();
        }

        public IMdSeminarThemesBOL CreateMdSeminarTheme(string mdSeminarTheme, bool isActive)
        {
            // Créer un nouvel enregistrement MdBank
            var newMdSeminarTheme = new MdSeminarThemes
            {
                Name = mdSeminarTheme,
                Active = isActive,
            };

            // Ajouter la nouvelle banque à la base de données
            Db.MdSeminarThemes.Add(newMdSeminarTheme);
            Db.SaveChanges();

            // Retourner le nouvel enregistrement sous forme de BOL
            return MapperWrapper.NewBol<MdSeminarThemesBOL>(newMdSeminarTheme);
        }
        #endregion

    }

        
}
