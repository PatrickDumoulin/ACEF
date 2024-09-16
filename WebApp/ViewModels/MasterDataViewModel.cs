using BusinessLayer.ViewModels;

namespace WebApp.ViewModels
{
    public class MasterDataViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDesjardins { get; set; } // Ajoutez cette propriété
    }
}
