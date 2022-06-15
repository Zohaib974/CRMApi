using CRMEntities.Models;
using CRMModels;
using CRMModels.DataTransfersObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IJobRepository
    {
        void CreateJob(Job job);
        PagedList<Job> GetJobs(JobParameters jobParameters, bool trackChanges);
        Task<Job> GetJobByIdAsync(long jobId, bool trackChanges);
        void MarkModified(Job job, UpdateJobDto jobDto);
        void CreateJobs(List<Job> jobs);       
    }
}
