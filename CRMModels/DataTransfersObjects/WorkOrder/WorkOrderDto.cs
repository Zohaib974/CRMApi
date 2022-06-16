using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class WorkOrderDto : CommonResponse
    {
        public long Id { get; set; }
        public EventPriorityEnum WorkOrderPriority
        {
            get
            {
                return (EventPriorityEnum)Priority;
            }
        }
        public int Priority { get; set; }
        public EventStatusEnum WorkOrderStatus
        {
            get
            {
                return (EventStatusEnum)Status;
            }
        }
        public int Status { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Notes { get; set; }
        public DateTime? LastStatusChangeDate { get; set; }
        public string Contact { get; set; }
        // ---------------To be configured in DB--------------------------
        //public ICollection<AssignedTeamMember> AssignedTeamMembers { get; set; }
        //public ICollection<Subcontractor> Subcontractors { get; set; }
        //public ICollection<LineItem> LineItems { get; set; }
    }
}
