using CRMEntities.Models;
using CRMModels;
using CRMModels.DataTransfersObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IActivityRepository
    {
        void CreateActivity(Activity entity);
        PagedList<Activity> GetActivities(ActivityParameters activityParameters, bool trackChanges);
        Task<Activity> GetActivityByIdAsync(long id, bool trackChanges);
        void MarkModified(Activity entity, UpdateActivityDto workOrderDto);
        void CreateActivities(List<Activity> entities);
    }
}
