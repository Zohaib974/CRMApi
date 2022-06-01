using CRMContracts;
using CRMEntities;
using CRMEntities.Models;
using CRMRepository.Extensions;
using CRMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMRepository
{
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {

        public AttachmentRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void AddAttchments(List<Attachment> attachments)
        {
            CreateEntities(attachments);
        }
        public PagedList<Attachment> GetAttachments(AttachmentParameters attachmentParameters, bool trackChanges)
        {
            var attachments = FindByCondition(e => !e.IsDeleted && e.UploadedBy == attachmentParameters.UploadedBy, trackChanges)
                                    .Search(attachmentParameters.SearchBy, attachmentParameters.SearchTerm)
                                    .Sort(attachmentParameters.OrderBy)
                                    .ToList();

            return PagedList<Attachment>.ToPagedList(attachments, attachmentParameters.PageNumber, attachmentParameters.PageSize);
        }
        public IEnumerable<Attachment> GetByIds(IEnumerable<long> ids, bool trackChanges) =>
                                     FindByCondition(x => ids.Contains(x.Id), trackChanges)
                                    .ToList();
    }
}
