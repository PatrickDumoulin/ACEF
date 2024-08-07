namespace WebApp.ViewModels
{
    public class ClientAttachmentViewModel
    {
        public int ClientId { get; set; }
        public int? IdEmployee { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
