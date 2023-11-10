using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class UserRole
    {
        public UserRole()
        {
            AccountUserRoles = new HashSet<AccountUserRole>();
            RoleUsers = new HashSet<RoleUser>();
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<AccountUserRole> AccountUserRoles { get; set; }
        public virtual ICollection<RoleUser> RoleUsers { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
