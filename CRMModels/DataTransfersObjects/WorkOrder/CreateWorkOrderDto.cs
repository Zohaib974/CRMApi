using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class CreateWorkOrderDto
    {
        [Required]
        public EventPriorityEnum WorkOrderPriority { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [Required]
        public EventStatusEnum WorkOrderStatus { get; set; }
        [Required]
        public TableType ReferenceType { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public string Notes { get; set; }
        public DateTime? LastStatusChangeDate { get; set; }
        //----------------Relational Fields-------------------------
        public long? ContactId { get; set; }
        public long? JobId { get; set; }
        // ---------------To be configured--------------------------
        //public ICollection<LineItem> LineItems { get; set; }
        //public ICollection<AssignedTeamMember> AssignedTeamMembers { get; set; }
        //public ICollection<Subcontractor> Subcontractors { get; set; }
    }
}
