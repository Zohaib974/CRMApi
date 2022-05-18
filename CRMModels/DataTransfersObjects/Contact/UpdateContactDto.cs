using CRMModels.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class UpdateContactDto
    {
        [Required]
        public long Id { get; set; }
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string FirstName { get; set; }
        [MaxLength(60, ErrorMessage = "Maximum length for the LastName is 60 characters.")]
        public string LastName { get; set; }
        public DateTime? LastStatusChangeDate { get; set; }
        [MaxLength(60, ErrorMessage = "Maximum length for the Company is 60 characters.")]
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ProfileImageLink { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Zip { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string FaxNumber { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string OfficeNumber { get; set; }
        //Additional Details
        [Required]
        [EnumDataType(typeof(ContactStatusEnum))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ContactStatusEnum ContactStatus { get; set; }
        public int Status
        {
            get
            {
                return (int)ContactStatus;
            }
        }
        public string DisplayName { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }

        //Relational Entities (Not Mapped to DB)
        //public string OfficeLocation { get; set; }
        //public string WorkFlow { get; set; }
        //public string SalesRep { get; set; }
        //public string Source { get; set; }
        //public string Subcontractors { get; set; }
        //public string RelatedContacts { get; set; }
        //public string AssignedTeamMembers { get; set; }
        //public string Tags { get; set; }
        public IFormFile File { get; set; }
    }
}
