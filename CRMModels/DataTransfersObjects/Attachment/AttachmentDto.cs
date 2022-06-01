namespace CRMModels.DataTransfersObjects
{
    public class AttachmentDto
    {
        public long Id { get; set; }
        public long UploadedBy { get; set; }
        public string FileName { get; set; }
        public string FileLink { get; set; }
        public string FileExension { get; set; }
        public string FileSize { get; set; }
        public string CreatedAt { get; set; }
    }
}
