using CoreLib.Injection;
using DataAccess.BOL.InterventionsInterventionSolutions;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL.Intervention;
using DataModels.BOL.InterventionsInterventionSolutions;
using DataModels.BOL.MdLoanReason;
using System.Collections.Generic;
using System.Linq;

public class InterventionBOL : AbstractBOL<Interventions>, IInterventionBOL
{


    public InterventionBOL() { }
    public InterventionBOL(Interventions record) : base(record) { }

    public int Id { get { return base.Record.Id; } set { base.Record.Id = value; } }

    public bool IsVirtual { get { return base.Record.IsVirtual ?? false; } set { base.Record.IsVirtual = value; } }

    public DateTime? DateIntervention { get { return base.Record.DateIntervention; } set { base.Record.DateIntervention = value; } }

    public int? IdEmployee { get { return base.Record.IdEmployee; } set { base.Record.IdEmployee = value; } }

    public int? IdClient { get { return base.Record.IdClient; } set { base.Record.IdClient = value; } }

    public int? IdReferenceType { get { return base.Record.IdReferenceType; } set { base.Record.IdReferenceType = value; } }

    public int? IdStatusType { get { return base.Record.IdStatusType; } set { base.Record.IdStatusType = value; } }

    public int? IdInterventionType { get { return base.Record.IdInterventionType; } set { base.Record.IdInterventionType = value; } }

    public byte[] DebtAmount { get { return base.Record.DebtAmount; } set { base.Record.DebtAmount = value; } }

    public int? IdLoanReason { get { return base.Record.IdLoanReason; } set { base.Record.IdLoanReason = value; } }

    public bool? IsLoanPaid { get { return base.Record.IsLoanPaid; } set { base.Record.IsLoanPaid = value; } }

    // Propriété pour le montant du prêt
    public decimal LoanAmount
    {
        get
        {
            if (base.Record.LoanAmount != null && base.Record.LoanAmount.Length > 0)
            {
                try
                {
                    return DecryptDebtAmount(base.Record.LoanAmount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la lecture de LoanAmount: {ex.Message}");
                    return 0m;
                }
            }
            return 0m;
        }
        set
        {
            try
            {
                if (value < 0)
                {
                    throw new ArgumentException("LoanAmount ne peut pas être négatif.");
                }

                base.Record.LoanAmount = EncryptDebtAmount(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'écriture de LoanAmount: {ex.Message}");
            }
        }
    }

    public decimal LoanAmountBalance
    {
        get
        {
            if (base.Record.LoanAmountBalance != null && base.Record.LoanAmountBalance.Length > 0)
            {
                try
                {
                    return DecryptDebtAmount(base.Record.LoanAmountBalance);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la lecture de LoanAmountBalance: {ex.Message}");
                    return 0m;
                }
            }
            return 0m;
        }
        set
        {
            try
            {
                if (value < 0)
                {
                    throw new ArgumentException("LoanAmountBalance ne peut pas être négatif.");
                }

                //[MB!!] - C'est extrêmement grave ce que vous faites là. Le champ n'est pas encrypté, il est simplement converti en byte[]. Il s'agit de données réelles et sensibles
                //le client pourrait se faire poursuivre si des montants financiers sont non protégés dans la base de données. Vous avez une méthode qui est fournie qui permet de gérer l'encyrption/décryption
                //voir CowMilkIngredientPriceListMonthBOL et la propriété Amount. S'il vous plait, repasser partout où vous êtiez supposé faire de l'encryption et assurez-vous que ce soit conforme.
                base.Record.LoanAmountBalance = EncryptDebtAmount(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'écriture de LoanAmountBalance: {ex.Message}");
            }
        }
    }

    //[MB] - À enlever.
    private byte[] EncryptDebtAmount(decimal amount)
    {
        return BitConverter.GetBytes((double)amount);
    }

    //[MB] - À enlever.
    private decimal DecryptDebtAmount(byte[] encryptedAmount)
    {
        return (decimal)BitConverter.ToDouble(encryptedAmount, 0);
    }





    // Propriété pour les IDs des solutions d'intervention
    public IEnumerable<int> InterventionSolutionsIds
    {
        get
        {
            return base.Record.InterventionsInterventionSolutions
            .Select(x => x.IdInterventionSolution);
        }
    }

    // Propriété pour la collection des solutions d'intervention
    public ICollection<IInterventionsInterventionSolutionsBOL> InterventionsInterventionSolutions
    {
        get
        {
            return base.Record.InterventionsInterventionSolutions
                .Select(x => new InterventionsInterventionSolutionsBOL(x))
                .Cast<IInterventionsInterventionSolutionsBOL>()
                .ToList();
        }
        set
        {
            base.Record.InterventionsInterventionSolutions = value.Select(x => new InterventionsInterventionSolutions
            {
                IdInterventionSolution = x.IdInterventionSolution,
                IdIntervention = this.Id
            }).ToList();
        }
    }

}
