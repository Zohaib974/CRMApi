using System;

namespace CRMEntities.Base
{
    public abstract class BaseEntity : IEntity
    {
        public virtual long Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        protected BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }


        public bool IsDeleted { get; set; }

    }
}
