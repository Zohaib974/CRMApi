using System;
using System.Collections.Generic;
using System.Text;

namespace CRMModels.Common
{
    public enum ContactStatusEnum
    {
        NotAssigned = 0,
        Waiting = 1,
        Check = 2
    }
    public enum JobStatusEnum
    {
        NotAssigned = 0,
        Waiting = 1,
        Check = 2
    }
    public enum EventStatusEnum
    {
        NotAssigned = 0,
        Incomplete = 1,
        Complete = 2
    }
    public enum EventPriorityEnum
    {
        NotAssigned = 0,
        High = 1,
        Meduim = 2,
        Low = 3
    }
    public enum EventTypeEnum
    {
        NotAssigned = 0,
        Task = 1
    }
    public enum ActivityTypeEnum
    {
        NotAssigned = 0,
        Task = 1
    }
    public enum TableType
    {
        NotAssigned = 0,
        Contacts = 1,
        Jobs = 2,
        Events =3
    }
}
