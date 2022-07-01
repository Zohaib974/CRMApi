using CRMEntities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMEntities.Models
{
    public class Activity : BaseEntity
    {
        [Required]
        public int Type { get; set; }
        [MaxLength(50)]
        public string Note { get; set; }
        [MaxLength(50)]
        public string Meeting{ get; set; }
        [MaxLength(50)]
        public string PhoneCall { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        //----------------Relational Fields-------------------------
        [ForeignKey(nameof(Contact))]
        public long? ContactId { get; set; }
        //---------------Navigation properties-----------------------
        public Contact Contact { get; set; }
    }
}
