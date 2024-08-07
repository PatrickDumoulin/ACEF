using CoreLib.Definitions;
using DataAccess.BOL.MdBank;
using DataAccess.BOL.MdEmploymentSituation;
using DataAccess.BOL.MdFamilySituation;
using DataAccess.BOL.MdGenderDenomination;
using DataAccess.BOL.MdHabitationType;
using DataAccess.BOL.MdInterventionSolution;
using DataAccess.BOL.MdInterventionStatusType;
using DataAccess.BOL.MdInterventionType;
using DataAccess.BOL.MdLoanReason;
using DataAccess.BOL.MdMaritalStatus;
using DataAccess.BOL.MdReferenceSource;
using DataAccess.BOL.MdScholarshipType;
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

namespace DataAccess.Providers.Entity
{
    public class MDEntityDAL : AcefEntityDAL, IMDDAL
    {
        public MDEntityDAL() { }
        public MDEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        public IMdBankBOL GetMdBank(int id)
        {
            MdBank record = Db.MdBank.FirstOrDefault (x => x.Id == id);
            return MapperWrapper.NewBol<MdBankBOL>(record);
        }

        public List<IMdBankBOL> GetAllMdBanks()
        {
            List<IRecord> records = Db.MdBank.ToList<IRecord>();
            return MapperWrapper.NewBols<MdBankBOL>(records).ToList<IMdBankBOL>();
        }


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
    }

        
}
