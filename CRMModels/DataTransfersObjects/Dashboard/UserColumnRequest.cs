using CRMModels.Common;
using System.ComponentModel.DataAnnotations;

namespace CRMModels.DataTransfersObjects
{
    public class UserColumnRequest
    {
        [Required]
        public TableType TableName { get; set; }
    }
}
