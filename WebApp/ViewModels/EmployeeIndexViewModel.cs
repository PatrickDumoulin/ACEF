namespace WebApp.ViewModels
{
    public class EmployeeIndexViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

}
