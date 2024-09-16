using BusinessLayer.ViewModels;

namespace WebApp.ViewModels
{
    public class MdGestionIndexViewModel
    {
        public List<string> MdNames { get; set; }
        public List<BusinessLayer.ViewModels.MasterDataViewModel> MdItems { get; set; }
    }
}
