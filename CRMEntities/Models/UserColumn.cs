using CRMEntities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMEntities.Models
{
    public class UserColumn : BaseEntity
    {
        [Required]
        public int TableType { get; set; }
        [MaxLength(20)]
        public string ColumnName { get; set; }
        [Required]
        public int ColumnOrder { get; set; }
        //----------------Relational Fields-------------------------
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        //---------------Navigation properties-----------------------
        public User User { get; set; }
    }
}
