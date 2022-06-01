namespace CRMModels
{
    public class AttachmentParameters : RequestParameters
    {
        public AttachmentParameters()
        {
            OrderBy = "createdOn";
        }
        public long UploadedBy { get; set; }
    }
}
