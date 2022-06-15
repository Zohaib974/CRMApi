using System.ComponentModel.DataAnnotations;

namespace CRMModels.DataTransfersObjects
{
    public class EmailAttachmentDto
    {
        [Required]
        public long DocumentId { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
