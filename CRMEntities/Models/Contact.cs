using CRMEntities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMEntities.Models
{
    public class Contact : BaseEntity
    {
        [Required(ErrorMessage = "FirstName name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string FirstName { get; set; }
        [MaxLength(60, ErrorMessage = "Maximum length for the LastName is 60 characters.")]
        public string LastName { get; set; }
        [MaxLength(60, ErrorMessage = "Maximum length for the Company is 60 characters.")]
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
        [MaxLength(60, ErrorMessage = "Maximum length for the DisplayName is 60 characters.")]
        public string DisplayName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        //Tracking Fields
        public DateTime? LastStatusChangeDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastContacted { get; set; }
        public int? ContactedCount { get; set; }

    }
}
