using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class CreateEventDto
    {
        [Required]
        public EventTypeEnum EventType { get; set; }
        [Required]
        public EventPriorityEnum EventPriority { get; set; }
        [MaxLength(50)]
        [Required]
        public string EventName { get; set; }
        [Required]
        public EventStatusEnum EventStatus { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
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
        // ---------------To be configured--------------------------
        //public ICollection<AssignedTeamMember> AssignedTeamMembers { get; set; }
        //public ICollection<Subcontractor> Subcontractors { get; set; }
        //public ICollection<Contact> RelatedContacts { get; set; }
        //public ICollection<Job> RelatedJobs { get; set; }
    }
}
