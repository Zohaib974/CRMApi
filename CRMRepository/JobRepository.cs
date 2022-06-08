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
    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {

        public JobRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void CreateJob(Job job) => Create(job);
        public void CreateJobs(List<Job> jobs)
        {
            CreateEntities(jobs);
        }
        public PagedList<Job> GetJobs(JobParameters jobParameters, bool trackChanges = false)
        {
            var jobs = FindByCondition(e => !e.IsDeleted,trackChanges)
                                    .Search(jobParameters.SearchBy, jobParameters.SearchTerm)
                                    .Sort(jobParameters.OrderBy)
                                    .ToList();

            return PagedList<Job>.ToPagedList(jobs, jobParameters.PageNumber, jobParameters.PageSize);
        }

        public async Task<Job> GetJobByIdAsync(long jobId, bool trackChanges)
        {
            return await FindByCondition(e => !e.IsDeleted && e.Id.Equals(jobId), trackChanges).SingleOrDefaultAsync();
        }
        #region helperMethod
        public void MarkModified(Job job,UpdateJobDto jobDto)
        {
            RepositoryContext.Entry(job).State = EntityState.Modified;
            var entityProperties = typeof(Job).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dtoProperties = typeof(UpdateJobDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(var dProp in dtoProperties)
            {
                 var dPropValue = dProp.GetValue(jobDto);
                if(dPropValue == null)
                {
                    var entityProperty = entityProperties.FirstOrDefault(pi => pi.Name.Equals(dProp.Name, StringComparison.InvariantCultureIgnoreCase));
                    if(entityProperty == null)
                        continue;
                    var epropName = entityProperty.Name.ToString();
                    RepositoryContext.Entry(job).Property(epropName).IsModified = false;
                }
            }
        }
        #endregion
    }
}
