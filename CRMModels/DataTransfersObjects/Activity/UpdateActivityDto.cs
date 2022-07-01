using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class UpdateActivityDto
    {
        [Required]
        public long Id { get; set; }
        public ActivityTypeEnum? ActivityType { get; set; }
        public int? Type
        {
            get
            {
               if (ActivityType != null)
                    return (int)ActivityType;
               return null;
            }
        }
        public string Note { get; set; }
        public string Meeting { get; set; }
        public string PhoneCall { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        //----------------Relational Fields-------------------------
        public long? ContactId { get; set; }
    }
}
