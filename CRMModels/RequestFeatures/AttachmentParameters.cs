namespace CRMModels
{
    public class AttachmentParameters : RequestParameters
    {
        public AttachmentParameters()
        {
            OrderBy = "createdOn";
        }
        //public long UploadedBy { get; set; }
        public long ContactId { get; set; }
        public bool IsImage { get; set; }
    }
}
