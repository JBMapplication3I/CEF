namespace SiteManager.ViewModels
{
    using System.Collections.Concurrent;

    public class RunSettingsViewModel : ViewModelBase
    {
        // ReSharper disable once StyleCop.SA1000 // Doesn't understand C# 9 implied new
        protected override ConcurrentDictionary<string, object?> Values { get; } = new()
        {
            [nameof(Version)] = "2021.1",
            [nameof(CEFDirectory)] = @"C:\Data\Projects\2021.1\_DEV\CEF\",
            [nameof(WEBDirectory)] = @"C:\Data\Projects\2021.1\_DEV\WEB9\",
            [nameof(PortalsRelativeToWEBDirectory)] = @"Portals\_default\Skins\",
            [nameof(ImagesDirectory)] = @"C:\Data\Projects\2021.1\_DEV\Images\",
            [nameof(SolutionItemsRelativeToCEFDirectory)] = @"Solution Items\",
            [nameof(SolutionName)] = "Clarity.Ecommerce.All.sln",
            [nameof(DataModelRelativeToCEFDirectory)] = @"01.DataAccessLayer\01.Clarity.Ecommerce.DataModel\",
            [nameof(ProjectAcronymAsCaps)] = @"CLARITY",
            [nameof(ProjectAcronymAsLowerCase)] = @"clarity",
            [nameof(EnvironmentAsLowerCase)] = @"local",
            [nameof(ProjectNameAsTitleCase)] = @"Clarity Ventures Demo Site",
            [nameof(CompanyName)] = @"Clarity Ventures, Inc.",
            [nameof(SkinName)] = @"Clarity",
            [nameof(MainDomainTitle)] = @"ClarityClient.com",
            [nameof(MainDomain)] = @"clarityclient.com",
            [nameof(SubDomain)] = @"clarity-local.",
            [nameof(FullDomain)] = @"clarity-local.clarityclient.com",
            [nameof(SiteName)] = @"ClarityClient.com (clarity-local.)",
            [nameof(SubAppsRoot)] = @"/DesktopModules/ClarityEcommerce",
            [nameof(AppNameDNN)] = @"DNN",
            [nameof(AppNameAPIPrefix)] = @"API-",
            [nameof(AppNameUIPrefix)] = @"UI-",
            [nameof(AppNameAdmin)] = @"Admin",
            [nameof(AppNameStorefront)] = @"Storefront",
            [nameof(AppNameBrandAdmin)] = @"BrandAdmin",
            [nameof(AppNameStoreAdmin)] = @"StoreAdmin",
            [nameof(AppNameVendorAdmin)] = @"VendorAdmin",
            [nameof(AppNameScheduler)] = @"Scheduler",
            [nameof(AppNameAPIReference)] = @"API-Reference",
            [nameof(AppNameFormat)] = "{{SiteName}} ({{Prefix}}{{Name}})",
            [nameof(DbSource)] = @".\SQL2019",
            [nameof(CEFDbName)] = @"DEMO_CEF_2021_1",
            [nameof(DNNDbName)] = @"DEMO_DNN9_2021_1",
            [nameof(DbAuthKind)] = @"User=SQLLogin;Password=p4ssw0rd;",
            [nameof(ConnectionStringFormat)] = "Data Source={{DbSource}};Initial Catalog={{DbName}};{{DbAuthKind}}",
            [nameof(IsRunning)] = false,
        };

        // ReSharper disable MultipleSpaces, MultipleStatementsOnOneLine, StyleCop.SA1025
#pragma warning disable format
        public string? Version                             { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? CEFDirectory                        { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? WEBDirectory                        { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? PortalsRelativeToWEBDirectory       { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? ImagesDirectory                     { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? SolutionItemsRelativeToCEFDirectory { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? SolutionName                        { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? DataModelRelativeToCEFDirectory     { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? ProjectAcronymAsCaps                { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? SkinName                            { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? ProjectAcronymAsLowerCase           { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? EnvironmentAsLowerCase              { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? ProjectNameAsTitleCase              { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? CompanyName                         { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? MainDomainTitle                     { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? MainDomain                          { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? SubDomain                           { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? FullDomain                          { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? SiteName                            { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? SubAppsRoot                         { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? AppNameDNN                          { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? AppNameAPIPrefix                    { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? AppNameUIPrefix                     { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? AppNameAdmin                        { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? AppNameStorefront                   { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? AppNameBrandAdmin                   { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? AppNameStoreAdmin                   { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? AppNameVendorAdmin                  { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? AppNameScheduler                    { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? AppNameAPIReference                 { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? AppNameFormat                       { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? DbSource                            { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? CEFDbName                           { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? DNNDbName                           { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? DbAuthKind                          { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public string? ConnectionStringFormat              { get => GetValue<string>(); set { if (GetValue<string>() == value) { return; } SetValue(value); OnPropertyChanged(); } }
        public bool IsRunning                              { get => GetValue<bool>();   set { if (GetValue<bool>()   == value) { return; } SetValue(value); OnPropertyChanged(); } }
#pragma warning restore format
        // ReSharper restore MultipleSpaces, MultipleStatementsOnOneLine, StyleCop.SA1025
    }
}
