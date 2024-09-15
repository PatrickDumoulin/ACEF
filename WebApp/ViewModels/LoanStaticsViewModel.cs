namespace WebApp.ViewModels
{
    public class LoanStatisticsViewModel
    {
        public int NumberOfLoansRequested { get; set; }
        public int NumberOfLoansGranted { get; set; }
        public decimal TotalLoanAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        public List<LoanReasonViewModel> LoanReasons { get; set; }
        public List<LoanByBankViewModel> LoansByBank { get; set; }
    }

    public class LoanReasonViewModel
    {
        public string Reason { get; set; }
        public int Count { get; set; }
    }

    public class LoanByBankViewModel
    {
        public string Bank { get; set; }
        public int Count { get; set; }
    }
}
