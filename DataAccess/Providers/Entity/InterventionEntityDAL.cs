using CoreLib.Definitions;
using DataAccess.BOL.Client;
using DataAccess.BOL.Seminar;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.Client;
using DataModels.BOL.Intervention;
using Microsoft.EntityFrameworkCore;
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
            var record = Db.Interventions
                           .Include(i => i.InterventionsInterventionSolutions) // Inclure les solutions d'intervention
                           .FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<InterventionBOL>(record);
        }


        public List<InterventionBOL> GetInterventions()
        {
            var records = Db.Interventions.Include(i => i.InterventionsInterventionSolutions).ToList();
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
                        LoanAmount = BitConverter.GetBytes((double)interventionBOL.LoanAmount),
                        LoanAmountBalance = BitConverter.GetBytes((double)interventionBOL.LoanAmountBalance)

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

            var existingIntervention = Db.Interventions
                .Include(i => i.InterventionsInterventionSolutions)
                .FirstOrDefault(x => x.Id == interventionBOL.Id);

            if (existingIntervention == null)
            {
                throw new KeyNotFoundException($"Intervention with ID {interventionBOL.Id} not found.");
            }

            // Mettre à jour les propriétés de l'intervention
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
            existingIntervention.LoanAmount = BitConverter.GetBytes((double)interventionBOL.LoanAmount);
            existingIntervention.LoanAmountBalance = BitConverter.GetBytes((double)interventionBOL.LoanAmountBalance);


            // Sauvegarder les changements
            Db.SaveChanges();
        }
        public void DeleteIntervention(int id)
        {
            var intervention = Db.Interventions.FirstOrDefault(i => i.Id == id);
            if (intervention != null)
            {
                Db.Interventions.Remove(intervention);
                Db.SaveChanges();
            }
        }
    }
}
