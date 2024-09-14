namespace WebApp.ViewModels
{
    public class UserStatisticsViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<StatisticViewModel> GenderStatistics { get; set; }
        public List<StatisticViewModel> AgeStatistics { get; set; }
        public List<StatisticViewModel> FamilySituationStatistics { get; set; }
        public List<StatisticViewModel> IncomeSourceStatistics { get; set; }
        public List<StatisticViewModel> NetIncomeStatistics { get; set; }
        public List<StatisticViewModel> ParticipationStatistics { get; set; }
    }
}
