using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMModels.DataTransfersObjects
{
    public class UploadAttachmentsDto
    {
        public long UploadedBy { get; set; }
        public long ContactId { get; set; }
        public bool IsImage { get; set; }
        public List<IFormFile> Files { get; set; }

    }
}
