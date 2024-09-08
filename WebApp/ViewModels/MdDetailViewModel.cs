using BusinessLayer.ViewModels;

namespace WebApp.ViewModels
{
    public class MdDetailViewModel
    {
        public string MdName { get; set; } 
        public List<MasterDataViewModel> MdItems { get; set; } 
    }
}
