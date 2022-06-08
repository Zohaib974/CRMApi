using CRMContracts;
using CRMEntities;
using CRMEntities.Models;
using CRMRepository.Extensions;
using CRMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRMRepository
{
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {

        public AttachmentRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void AddAttchmentsAsync(List<Attachment> attachments)
        {
            CreateEntities(attachments);
        }
        public PagedList<Attachment> GetAttachments(AttachmentParameters attachmentParameters, bool trackChanges)
        {
            var attachments = FindByCondition(e => !e.IsDeleted && (attachmentParameters.ContactId ==0 || e.ContactId == attachmentParameters.ContactId) && e.isImageFile == attachmentParameters.IsImage, trackChanges)
                                    .Search(attachmentParameters.SearchBy, attachmentParameters.SearchTerm)
                                    .Sort(attachmentParameters.OrderBy)
                                    .ToList();

            return PagedList<Attachment>.ToPagedList(attachments, attachmentParameters.PageNumber, attachmentParameters.PageSize);
        }
        public async Task<List<Attachment>> GetByIdsAsync(IEnumerable<long> ids, bool trackChanges)
        {
            return await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public async Task<Attachment> GetByIdAsync(long id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == id, trackChanges).FirstOrDefaultAsync();
        }
    }
}
