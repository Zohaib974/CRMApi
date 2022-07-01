using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class CreateJobDto
    {
        [Required(ErrorMessage = "Name name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Address1 { get; set; }
        [MaxLength(200)]
        public string Address2 { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        public int Zip { get; set; }
        //Additional Details
        [Required]
        public JobStatusEnum JobStatus { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string LeadSource { get; set; }
        public string Tags { get; set; }
        //Tracking Fields
        public DateTime? LastStatusChangeDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        //----------------Relational Fields-------------------------
        public long? PrimaryContactId { get; set; }
        public long? CompanyId { get; set; }
        // ---------------To be configured in DB--------------------------
        public long OfficeLocationId { get; set; }
        public long WorkFlowId { get; set; }
        public long SalesRepId { get; set; }
        public long ProductioManagerId { get; set; }
        public long TimelineId { get; set; }
        //public ICollection<AssignedTeamMember> AssignedTeamMembers { get; set; }
        //public ICollection<Subcontractor> Subcontractors { get; set; }
        //public ICollection<Contact> RelatedContacts { get; set; }
    }
}
