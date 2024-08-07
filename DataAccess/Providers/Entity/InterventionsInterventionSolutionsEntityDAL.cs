using DataAccess.BOL.Intervention;
using DataAccess.BOL.InterventionsInterventionSolutions;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.Intervention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Entity
{
    public class InterventionsInterventionSolutionsEntityDAL : AcefEntityDAL, IInterventionsInterventionSolutionsDAL
    {
        public InterventionsInterventionSolutionsEntityDAL() { }
        public InterventionsInterventionSolutionsEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        

        public InterventionsInterventionSolutionsBOL GetInterventionsInterventionSolution(int id)
        {
            InterventionsInterventionSolutions record = Db.InterventionsInterventionSolutions.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<InterventionsInterventionSolutionsBOL>(record);
        }

        public List<InterventionsInterventionSolutionsBOL> GetInterventionsInterventionSolutions()
        {
            var records = Db.InterventionsInterventionSolutions.ToList();
            return records.Select(x => new InterventionsInterventionSolutionsBOL(x)).ToList();
        }

        public void CreateInterventionsInterventionSolutions(InterventionsInterventionSolutionsBOL interventionsInterventionSolutionsBOL)
        {
            if (interventionsInterventionSolutionsBOL == null)
            {
                throw new ArgumentNullException(nameof(InterventionsInterventionSolutionsBOL), "InterventionsInterventionSolutionsBOL cannot be null");
            }

            var newInterventionsInterventionSolutions = new InterventionsInterventionSolutions
            {
                IdIntervention = interventionsInterventionSolutionsBOL.IdIntervention,
                IdInterventionSolution = interventionsInterventionSolutionsBOL.IdInterventionSolution,
               
            };

            Db.InterventionsInterventionSolutions.Add(newInterventionsInterventionSolutions);
            Db.SaveChanges();
        }   

        public void UpdateInterventionsInterventionSolutions(InterventionsInterventionSolutionsBOL interventionsInterventionSolutionsBOL)
        {
            if (interventionsInterventionSolutionsBOL == null)
            {
                throw new ArgumentNullException(nameof(interventionsInterventionSolutionsBOL), "InterventionsInterventionSolutionsBOL cannot be null");
            }

            var existingInterventionsInterventionSolutions= Db.InterventionsInterventionSolutions.FirstOrDefault(x => x.Id == interventionsInterventionSolutionsBOL.Id);

            if (existingInterventionsInterventionSolutions == null)
            {
                throw new KeyNotFoundException($"InterventionsInterventionSolutions with ID {interventionsInterventionSolutionsBOL.Id} not found.");
            }

            existingInterventionsInterventionSolutions.IdIntervention = interventionsInterventionSolutionsBOL.IdIntervention;
            existingInterventionsInterventionSolutions.IdIntervention = interventionsInterventionSolutionsBOL.IdIntervention;
            


            Db.SaveChanges();
        }

        

        public void DeleteInterventionsInterventionSolutions(int id)
        {
            var interventionsInterventionSolutions = Db.InterventionsInterventionSolutions.FirstOrDefault(i => i.Id == id);
            if (interventionsInterventionSolutions != null)
            {
                Db.InterventionsInterventionSolutions.Remove(interventionsInterventionSolutions);
                Db.SaveChanges();
            }
        }
    }
}
