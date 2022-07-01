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
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {

        public ActivityRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void CreateActivity(Activity entity) => Create(entity);
        public void CreateActivities(List<Activity> entities)
        {
            CreateEntities(entities);
        }
        public PagedList<Activity> GetActivities(ActivityParameters activityParameters, bool trackChanges = false)
        {
            var events = FindByCondition(e => !e.IsDeleted && (e.ContactId != null
                                    && e.ContactId.Value == activityParameters.ContactId), trackChanges)
                                    .Search(activityParameters.SearchBy, activityParameters.SearchTerm)
                                    .Sort(activityParameters.OrderBy)
                                    .ToList();

            return PagedList<Activity>.ToPagedList(events, activityParameters.PageNumber, activityParameters.PageSize);
        }

        public async Task<Activity> GetActivityByIdAsync(long id, bool trackChanges)
        {
            return await FindByCondition(e => !e.IsDeleted && e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
        #region helperMethod
        public void MarkModified(Activity entity, UpdateActivityDto activityDto)
        {
            RepositoryContext.Entry(entity).State = EntityState.Modified;
            var entityProperties = typeof(Activity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dtoProperties = typeof(UpdateActivityDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(var dProp in dtoProperties)
            {
                 var dPropValue = dProp.GetValue(activityDto);
                if(dPropValue == null)
                {
                    var entityProperty = entityProperties.FirstOrDefault(pi => pi.Name.Equals(dProp.Name, StringComparison.InvariantCultureIgnoreCase));
                    if(entityProperty == null)
                        continue;
                    var epropName = entityProperty.Name.ToString();
                    RepositoryContext.Entry(entity).Property(epropName).IsModified = false;
                }
            }
        }
        #endregion
    }
}
