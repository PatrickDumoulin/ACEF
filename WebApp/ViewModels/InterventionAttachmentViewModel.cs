namespace WebApp.ViewModels
{
    public class InterventionAttachmentViewModel
    {
        

        public int? IdIntervention { get; set; }

        public int? IdEmployee { get; set; }

        public string FileName { get; set; }

        public byte[] FileContent { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
