using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Handler
{
    public class MasterDataHandler
    {
        private readonly IMDDAL _mdDal;

        public MasterDataHandler(IMDDAL mdDal)
        {
            _mdDal = mdDal ?? throw new ArgumentNullException(nameof(mdDal));
        }

        // Méthode pour récupérer une liste d'entités MD basée sur le nom de l'entité
        public object GetAllMdDetailsByName(string name)
        {
            var listMethodsMap = new Dictionary<string, Func<object>>()
        {
            { nameof(MdBank), _mdDal.GetAllMdBanks },
            { nameof(MdEmploymentSituation), _mdDal.GetAllMdEmploymentSituations },
            { nameof(MdFamilySituation), _mdDal.GetAllMdFamilySituations },
            { nameof(MdGenderDenomination), _mdDal.GetAllMdGenderDenominations },
            { nameof(MdHabitationType), _mdDal.GetAllMdHabitationTypes },
            { nameof(MdIncomeType), _mdDal.GetAllMdIncomeTypes },
            { nameof(MdInterventionSolution), _mdDal.GetAllMdInterventionSolutions },
            { nameof(MdInterventionType), _mdDal.GetAllMdInterventionTypes },
            { nameof(MdLoanReason), _mdDal.GetAllMdLoanReasons },
            { nameof(MdMaritalStatus), _mdDal.GetAllMdMaritalStatus },
            { nameof(MdReferenceSource), _mdDal.GetAllMdReferenceSources },
            { nameof(MdScholarshipType), _mdDal.GetAllMdScholarshipTypes },
            { nameof(MdSeminarThemes), _mdDal.GetAllMdSeminarThemes }
        };

            if (listMethodsMap.ContainsKey(name))
            {
                return listMethodsMap[name].Invoke();
            }

            throw new ArgumentException($"L'entité avec le nom {name} n'est pas prise en charge.");
        }

        // Vous pouvez aussi ajouter une méthode pour récupérer un MD par ID
        public object GetMdDetailsById(string name, int id)
        {
            var getMethodMap = new Dictionary<string, Func<int, object>>()
        {
            { nameof(MdBank), _mdDal.GetMdBank },
            { nameof(MdEmploymentSituation), _mdDal.GetMdEmploymentSituation },
            { nameof(MdFamilySituation), _mdDal.GetMdFamilySituation },
            { nameof(MdGenderDenomination), _mdDal.GetMdGenderDenomination },
            { nameof(MdHabitationType), _mdDal.GetMdHabitationType },
            { nameof(MdIncomeType), _mdDal.GetMdIncomeType },
            { nameof(MdInterventionSolution), _mdDal.GetMdInterventionSolution },
            { nameof(MdInterventionType), _mdDal.GetMdInterventionType },
            { nameof(MdLoanReason), _mdDal.GetMdLoanReason },
            { nameof(MdMaritalStatus), _mdDal.GetMdMaritalStatus },
            { nameof(MdReferenceSource), _mdDal.GetMdReferenceSource },
            { nameof(MdScholarshipType), _mdDal.GetMdScholarshipType },
            { nameof(MdSeminarThemes), _mdDal.GetMdSeminarTheme }
        };

            if (getMethodMap.ContainsKey(name))
            {
                return getMethodMap[name].Invoke(id);
            }

            throw new ArgumentException($"L'entité avec le nom {name} n'est pas prise en charge.");
        }
    }

}
