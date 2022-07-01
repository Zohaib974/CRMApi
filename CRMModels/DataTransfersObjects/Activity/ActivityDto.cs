using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class ActivityDto : CommonResponse
    {
        public long Id { get; set; }
        public ActivityTypeEnum ActivityType
        {
            get
            {
                return (ActivityTypeEnum)Type;
            }
        }
        public int Type { get; set; }
        public string Note { get; set; }
        public string Meeting { get; set; }
        public string PhoneCall { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }
    }
}
