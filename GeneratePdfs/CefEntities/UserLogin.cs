using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class UserLogin
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
