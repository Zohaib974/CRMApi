using CRMModels.Common;
using System.ComponentModel.DataAnnotations;

namespace CRMModels.DataTransfersObjects
{
    public class CreateUserColumnDto
    {
        [Required]
        public TableType TableName { get; set; }
        [MaxLength(20)]
        public string ColumnName { get; set; }
        [Required]
        public int ColumnOrder { get; set; }
    }
}
