namespace WebApp.ViewModels
{
    public class RepartitionViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<ReferenceDistributionViewModel> ReferenceDistribution { get; set; }
        public List<DesjardinsDetailViewModel> DesjardinsDetails { get; set; }
        public List<ClientByBankViewModel> ClientsByBank { get; set; }
    }

    public class ReferenceDistributionViewModel
    {
        public string ReferenceName { get; set; }
        public int Count { get; set; }
    }

    public class DesjardinsDetailViewModel
    {
        public string BankName { get; set; }
        public int Count { get; set; }
    }

    public class ClientByBankViewModel
    {
        public string BankName { get; set; }
        public int Count { get; set; }
    }
}
