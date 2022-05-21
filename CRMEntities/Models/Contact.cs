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
        public int Status { get; set; }
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
        public bool? IsImported { get; set; }

    }
}
