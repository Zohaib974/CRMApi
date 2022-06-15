using CRMEntities.Models;
using CRMModels;
using CRMModels.DataTransfersObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IEventRepository
    {
        void CreateEvent(Event eventEty);
        PagedList<Event> GetEvents(EventParameters eventParameters, bool trackChanges);
        Task<Event> GetEventByIdAsync(long eventId, bool trackChanges);
        void MarkModified(Event eventEty, UpdateEventDto eventDto);
        void CreateEvents(List<Event> events);
    }
}
