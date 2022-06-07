using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        public string CreatedOn { get; set; }
    }
    public class AttachmentGroups 
    {
        public long UploadedBy { get; set; }
        public string Name { get; set; } = "Jon Doe";
        public List<AttachmentDto> attachments { get; set; }
    }
}
