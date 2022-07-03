using CRMContracts;
using CRMEntities;
using CRMEntities.Models;
using CRMModels;
using CRMModels.DataTransfersObjects;
using CRMRepository.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CRMRepository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {

        public EventRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void CreateEvent(Event eventEty) => Create(eventEty);
        public void CreateEvents(List<Event> events)
        {
            CreateEntities(events);
        }
        public PagedList<Event> GetEvents(EventParameters eventParameters, bool trackChanges = false)
        {
            var events = FindByCondition(e => !e.IsDeleted && (e.ContactId != null
                                    && e.ContactId.Value == eventParameters.ContactId), trackChanges)
                                    .Search(eventParameters.SearchBy, eventParameters.SearchTerm)
                                    .Sort(eventParameters.OrderBy)
                                    .ToList();

            return PagedList<Event>.ToPagedList(events, eventParameters.PageNumber, eventParameters.PageSize);
        }

        public async Task<Event> GetEventByIdAsync(long EventId, bool trackChanges)
        {
            return await FindByCondition(e => !e.IsDeleted && e.Id.Equals(EventId), trackChanges).Include(a=>a.EventContacts).ThenInclude(c=>c.Contact).SingleOrDefaultAsync();
        }
        #region helperMethod
        public void MarkModified(Event Event,UpdateEventDto EventDto)
        {
            RepositoryContext.Entry(Event).State = EntityState.Modified;
            var entityProperties = typeof(Event).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dtoProperties = typeof(UpdateEventDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(var dProp in dtoProperties)
            {
                 var dPropValue = dProp.GetValue(EventDto);
                if(dPropValue == null)
                {
                    var entityProperty = entityProperties.FirstOrDefault(pi => pi.Name.Equals(dProp.Name, StringComparison.InvariantCultureIgnoreCase));
                    if(entityProperty == null)
                        continue;
                    var epropName = entityProperty.Name.ToString();
                    RepositoryContext.Entry(Event).Property(epropName).IsModified = false;
                }
            }
        }
        #endregion
    }
}
