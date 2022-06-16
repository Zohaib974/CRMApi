using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class UpdateWorkOrderDto
    {
        [Required]
        public long Id { get; set; }
        public EventPriorityEnum? WorkOrderPriority { get; set; }
        public int? Priority
        {
            get
            {
               if (WorkOrderPriority != null)
                    return (int)WorkOrderPriority;
               return null;
            }
        }
        [MaxLength(50)]
        public string Name { get; set; }
        public EventStatusEnum? WorkOrderStatus { get; set; }
        public int? Status
        {
            get
            {
               if (WorkOrderStatus != null)
                    return  (int)WorkOrderStatus;
                return null;
            }
        }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Notes { get; set; }
        public DateTime? LastStatusChangeDate { get; set; }
        //----------------Relational Fields-------------------------
        public long? ContactId { get; set; }
        // ---------------To be configured in DB--------------------------
        //public ICollection<AssignedTeamMember> AssignedTeamMembers { get; set; }
        //public ICollection<Subcontractor> Subcontractors { get; set; }
        //public ICollection<LineItem> LineItems { get; set; }
    }
}
