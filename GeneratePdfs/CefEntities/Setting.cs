using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Setting
    {
        public Setting()
        {
            ScheduledJobConfigurationSettings = new HashSet<ScheduledJobConfigurationSetting>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int? StoreId { get; set; }
        public string Value { get; set; } = null!;
        public int TypeId { get; set; }
        public int? SettingGroupId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public int? BrandId { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual SettingGroup? SettingGroup { get; set; }
        public virtual Store? Store { get; set; }
        public virtual SettingType Type { get; set; } = null!;
        public virtual ICollection<ScheduledJobConfigurationSetting> ScheduledJobConfigurationSettings { get; set; }
    }
}
