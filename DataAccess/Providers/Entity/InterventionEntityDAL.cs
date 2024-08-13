using CoreLib.Definitions;
using DataAccess.BOL.Client;
using DataAccess.BOL.Seminar;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.Client;
using DataModels.BOL.Intervention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Entity
{
    public class InterventionEntityDAL : AcefEntityDAL, IInterventionDAL
    {
        public InterventionEntityDAL() { }
        public InterventionEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        

        public InterventionBOL GetIntervention(int id)
        {
            Interventions record = Db.Interventions.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<InterventionBOL>(record);
        }

        public List<InterventionBOL> GetInterventions()
        {
            var records = Db.Interventions.ToList();
            return records.Select(x => new InterventionBOL(x)).ToList();
        }

        public void CreateIntervention(InterventionBOL interventionBOL)
        {
            if (interventionBOL == null)
            {
                throw new ArgumentNullException(nameof(interventionBOL), "InterventionBOL cannot be null");
            }

            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    var newIntervention = new Interventions
                    {
                        IsVirtual = interventionBOL.IsVirtual,
                        DateIntervention = interventionBOL.DateIntervention,
                        IdEmployee = interventionBOL.IdEmployee,
                        IdClient = interventionBOL.IdClient,
                        IdReferenceType = interventionBOL.IdReferenceType,
                        IdStatusType = interventionBOL.IdStatusType,
                        IdInterventionType = interventionBOL.IdInterventionType,
                        DebtAmount = interventionBOL.DebtAmount,
                        IdLoanReason = interventionBOL.IdLoanReason,
                        IsLoanPaid = interventionBOL.IsLoanPaid,
                    };

                    Db.Interventions.Add(newIntervention);
                    Db.SaveChanges();

                    // Mettre à jour l'ID de l'objet BOL après l'insertion
                    interventionBOL.Id = newIntervention.Id;

                    // Ajouter les solutions associées
                    foreach (var solution in interventionBOL.InterventionsInterventionSolutions)
                    {
                        var interventionSolution = new InterventionsInterventionSolutions
                        {
                            IdIntervention = newIntervention.Id,
                            IdInterventionSolution = solution.IdInterventionSolution
                        };
                        Db.InterventionsInterventionSolutions.Add(interventionSolution);
                    }

                    Db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        public void UpdateIntervention(InterventionBOL interventionBOL)
        {
            if (interventionBOL == null)
            {
                throw new ArgumentNullException(nameof(interventionBOL), "InterventionBOL cannot be null");
            }

            var existingIntervention = Db.Interventions.FirstOrDefault(x => x.Id == interventionBOL.Id);

            if (existingIntervention == null)
            {
                throw new KeyNotFoundException($"Intervention with ID {interventionBOL.Id} not found.");
            }

            existingIntervention.IsVirtual = interventionBOL.IsVirtual;
            existingIntervention.DateIntervention = interventionBOL.DateIntervention;
            existingIntervention.IdEmployee = interventionBOL.IdEmployee;
            existingIntervention.IdClient = interventionBOL.IdClient;
            existingIntervention.IdReferenceType = interventionBOL.IdReferenceType;
            existingIntervention.IdStatusType = interventionBOL.IdStatusType;
            existingIntervention.IdInterventionType = interventionBOL.IdInterventionType;
            existingIntervention.DebtAmount = interventionBOL.DebtAmount;
            existingIntervention.IdLoanReason = interventionBOL.IdLoanReason;
            existingIntervention.IsLoanPaid = interventionBOL.IsLoanPaid;
            

            Db.SaveChanges();
        }
        public void DeleteIntervention(int id)
        {
            var intervention = Db.Interventions.FirstOrDefault(i=>i.Id == id);
            if (intervention != null)
            {
                Db.Interventions.Remove(intervention);
                Db.SaveChanges();
            }
        }
    }
}
