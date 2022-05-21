using CRMModels.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class CreateContactDto
    {

        [Required(ErrorMessage = "FirstName name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string FirstName { get; set; }
        [MaxLength(60, ErrorMessage = "Maximum length for the LastName is 60 characters.")]
        public string LastName { get; set; }
        [MaxLength(60, ErrorMessage = "Maximum length for the Company is 60 characters.")]
        public string Company { get; set; }
        [MaxLength(200)]
        public string Address1 { get; set; }
        [MaxLength(200)]
        public string Address2 { get; set; }
        public string ProfileImageLink { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        public int Zip { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Website { get; set; }
        [MaxLength(20)]
        public string FaxNumber { get; set; }
        [MaxLength(15)]
        public string MobileNumber { get; set; }
        [MaxLength(15)]
        public string HomeNumber { get; set; }
        [MaxLength(15)]
        public string OfficeNumber { get; set; }
        //Additional Details
        [Required]
        [EnumDataType(typeof(ContactStatusEnum))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ContactStatusEnum ContactStatus { get; set; }
        [MaxLength(60, ErrorMessage = "Maximum length for the DisplayName is 60 characters.")]
        public string DisplayName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(200)]
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
        [Required]
        public IFormFile File { get; set; }
    }
}
