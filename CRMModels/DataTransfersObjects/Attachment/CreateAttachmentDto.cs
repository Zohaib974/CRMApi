using System;
using System.Collections.Generic;
using System.Text;

namespace CRMModels.DataTransfersObjects
{
    public class CreateAttachmentDto
    {
        public long UploadedBy { get; set; }
        public long ContactId { get; set; }
        public bool IsUploaded { get; set; }
        public string FileName { get; set; }
        public string FileLink { get; set; }
        public string FileExension { get; set; }
        public string FileSize { get; set; }
        public bool isImageFile { get; set; }
    }
}
