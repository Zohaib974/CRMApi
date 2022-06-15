using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class UpdateJobDto
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        //Additional Details
        public JobStatusEnum? JobStatus { get; set; }
        public int? Status
        {
            get
            {
                if(JobStatus != null)
                    return (int)JobStatus;
                return null;
            }
        }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public string LeadSource { get; set; }
        public string Tags { get; set; }
        //Tracking Fields
        public DateTime? LastStatusChangeDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        //----------------Relational Fields-------------------------
        public long? PrimaryContactId { get; set; }
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
