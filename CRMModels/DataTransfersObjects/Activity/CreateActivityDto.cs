using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class CreateActivityDto
    {
        [Required]
        public ActivityTypeEnum ActivityType { get; set; }
        [MaxLength(50)]
        public string Note { get; set; }
        [MaxLength(50)]
        public string Meeting { get; set; }
        [MaxLength(50)]
        public string PhoneCall { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        //----------------Relational Fields-------------------------
        public long? ContactId { get; set; }
    }
}
