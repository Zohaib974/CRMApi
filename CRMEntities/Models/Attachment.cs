using CRMEntities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMEntities.Models
{
    public class Attachment : BaseEntity
    {
        [Required]
        public long UploadedBy { get; set; }
        [Required]
        [ForeignKey(nameof(Contact))]
        public long ContactId { get; set; }
        [MaxLength(200)]
        public string FileName { get; set; }
        public string FileLink { get; set; }
        [MaxLength(20)]
        public string FileExension { get; set; }
        [MaxLength(20)]
        public string FileSize { get; set; }
        public bool isImageFile { get; set; }
        public bool IsUploaded { get; set; }
        public Contact Contact { get; set; }

    }
}
