using CRMModels.Common;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRMModels.DataTransfersObjects
{
    public class EventDto : CommonResponse
    {
        public EventDto()
        {
            RelatedContacts = new List<ContactDto>();
        }
        public long Id { get; set; }
        public EventTypeEnum EventType
        {
            get
            {
                return (EventTypeEnum)Type;
            }
        }
        public int Type { get; set; }
        public EventPriorityEnum EventPriority
        {
            get
            {
                return (EventPriorityEnum)Priority;
            }
        }
        public int Priority { get; set; }
        public EventStatusEnum EventStatus
        {
            get
            {
                return (EventStatusEnum)Status;
            }
        }
        public int Status { get; set; }
        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EstimatedDuration { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public DateTime? LastStatusChangeDate { get; set; }
        public string Contact { get; set; }
        // ---------------To be configured in DB--------------------------
        //public ICollection<AssignedTeamMember> AssignedTeamMembers { get; set; }
        //public ICollection<Subcontractor> Subcontractors { get; set; }
        public ICollection<ContactDto> RelatedContacts { get; set; }
    }
}
