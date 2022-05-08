using CRMEntities.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace CRMEntities.Models
{
    public class User : IdentityUser<long>, IEntity
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
    #region IdentityOverrides
    public class ApplicationUserRole : IdentityUserRole<long>
    {
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        //public virtual User User { get; set; }
        //public virtual UserRole Role { get; set; }
    }
    public class UserRole : IdentityRole<long>, IEntity
	{
        public UserRole()
        {
            RoleClaims = new HashSet<RoleClaim>();
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }
        public bool IsPublic { get; set; }
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
    public class RoleClaim : IdentityRoleClaim<long>
    {
        //public virtual UserRole Role { get; set; }
    }
    #endregion
}
