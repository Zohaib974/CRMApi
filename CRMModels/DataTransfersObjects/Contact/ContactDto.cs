using CRMModels.Common;
using System;

namespace CRMModels.DataTransfersObjects
{
    public class ContactDto : CommonResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ProfileImageLink { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string FaxNumber { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string OfficeNumber { get; set; }
        //Additional Details
        public int Status { get; set; }
        public string DisplayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        //Tracking Fields
        public DateTime? LastStatusChangeDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastContacted { get; set; }
        public int? ContactedCount { get; set; }

        //Relational Entities (Not Mapped to DB)
        public string OfficeLocation { get; set; }
        public string WorkFlow { get; set; }
        public string SalesRep { get; set; }
        public string Source { get; set; }
        public string Subcontractors { get; set; }
        public string RelatedContacts { get; set; }
        public string AssignedTeamMembers { get; set; }
        public string Tags { get; set; }
    }
}
