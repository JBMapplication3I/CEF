using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Permission
    {
        public Permission()
        {
            Roles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
    }
}
