using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class UpdateEventDto
    {
        [Required]
        public long Id { get; set; }
        public EventTypeEnum? EventType { get; set; }
        public int? Type
        {
            get
            {
                if (EventType != null)
                    return (int)EventType;
                return null;
            }
        }
        public EventPriorityEnum? EventPriority { get; set; }
        public int? Priority
        {
            get
            {
               if (EventPriority != null)
                    return (int)EventPriority;
               return null;
            }
        }
        [MaxLength(50)]
        public string EventName { get; set; }
        public EventStatusEnum? EventStatus { get; set; }
        public int? Status
        {
            get
            {
               if (EventStatus != null)
                    return  (int)EventStatus;
                return null;
            }
        }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [MaxLength(50)]
        public string EstimatedDuration { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(200)]
        public string Tags { get; set; }
        public DateTime? LastStatusChangeDate { get; set; }
        //----------------Relational Fields-------------------------
        public long? ContactId { get; set; }
        public long? CompanyId { get; set; }
        // ---------------To be configured in DB--------------------------
        //public ICollection<AssignedTeamMember> AssignedTeamMembers { get; set; }
        //public ICollection<Subcontractor> Subcontractors { get; set; }
        //public ICollection<Contact> RelatedContacts { get; set; }
    }
}
