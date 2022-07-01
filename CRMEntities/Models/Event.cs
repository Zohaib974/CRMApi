using CRMEntities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMEntities.Models
{
    public class Event : BaseEntity
    {
        public Event()
        {
            EventContacts = new List<EventContact>();
        }
        [Required]
        public int Type { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public int Status { get; set; }
        [MaxLength(50)]
        [Required]
        public string EventName { get; set; }
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
        //Tracking Fields
        public DateTime? LastStatusChangeDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        //----------------Relational Fields-------------------------
        [ForeignKey(nameof(Contact))]
        public long? ContactId { get; set; }
        public long? CompanyId { get; set; }
        // ---------------To be configured--------------------------
        //public ICollection<AssignedTeamMember> AssignedTeamMembers { get; set; }
        //public ICollection<Subcontractor> Subcontractors { get; set; }
        //public ICollection<Job> RelatedJobs { get; set; }
        //---------------Navigation properties-----------------------
        public Contact Contact { get; set; }
        public ICollection<EventContact> EventContacts { get; set; }
    }
    public class EventContact
    {
        public long ContactId { get; set; }
        public Contact Contact { get; set; }
        public long EventId { get; set; }
        public Event Event { get; set; }
    }
}
