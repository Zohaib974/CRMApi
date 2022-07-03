using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class JobDto : CommonResponse
    {
        public JobDto()
        {
            RelatedContacts = new List<ContactDto>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public int Status { get; set; }
        public JobStatusEnum JobStatus
        {
            get
            {
                return (JobStatusEnum)Status;
            }
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string LeadSource { get; set; }
        public string Tags { get; set; }
        public DateTime? LastStatusChangeDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public string PrimaryContact { get; set; }
        public string OfficeLocation { get; set; }
        public string WorkFlow { get; set; }
        public string SalesRep { get; set; }
        public string ProductioManager { get; set; }
        public string Timeline { get; set; }
        //public ICollection<AssignedTeamMember> AssignedTeamMembers { get; set; }
        //public ICollection<Subcontractor> Subcontractors { get; set; }
        public ICollection<ContactDto> RelatedContacts { get; set; }
    }
}
