// <copyright file="ClarityEcommerceEntities.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the clarity ecommerce entities class</summary>
#pragma warning disable CS8600,CS8602,CS8603,CS8618 // These things are handled by EF internally
#if ORACLE
namespace Clarity.Ecommerce.DataModel.Oracle
#else
namespace Clarity.Ecommerce.DataModel
#endif
{
#if ORACLE
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using System.Data.Entity;
    using DataModel;
    using Microsoft.AspNet.Identity.EntityFramework;
#else
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RichardLawley.EF.AttributeConfig;
#endif

#if ORACLE
    [DbConfigurationType(typeof(Migrations.OracleClarityEFConfigurations))]
    public partial class OracleClarityEcommerceEntities
#else
    public partial class ClarityEcommerceEntities
#endif
        : IdentityDbContext<User, UserRole, int, UserLogin, RoleUser, UserClaim>
    {
#if ORACLE
        private static readonly string OracleUser = ConfigurationManager.AppSettings["OracleUser"];

        public OracleClarityEcommerceEntities()
            : base("OracleClarityEcommerceEntities")
        {
            Database.SetInitializer(new Migrations.OracleModelDBInitializer());
            Database.CommandTimeout = 10 * 60;
        }

        public OracleClarityEcommerceEntities(string name)
            : base(name)
        {
            Database.SetInitializer(new Migrations.OracleModelDBInitializer());
            Database.CommandTimeout = 10 * 60;
        }
#else
#if NET5_0_OR_GREATER
        private static readonly string DefaultConnectionString;

        static ClarityEcommerceEntities()
        {
            DefaultConnectionString = "Data Source=.\\SQL2019"
                + ";Initial Catalog=CEF_2020_3_DEV_NET5_new"
                + ";Integrated Security=true"
                + ";Persist Security Info=True"
                + ";MultipleActiveResultSets=true";
            if (!System.IO.File.Exists(@".\appSettings.json"))
            {
                // throw new InvalidOperationException("appSettings.json doesn't exist");
                return;
            }
            // ReSharper disable ThrowExceptionInUnexpectedLocation
            DefaultConnectionString = ((Newtonsoft.Json.Linq.JObject.Parse(
                        System.IO.File.ReadAllText(@".\appSettings.ConnectionStrings.json"))["ConnectionStrings"]
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    ?? throw new InvalidOperationException("appSettings.json::ConnectionStrings is null"))["ClarityEcommerceEntities"]
                ?? throw new InvalidOperationException("appSettings.json::ConnectionStrings::ClarityEcommerceEntities is null"))
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
                .ToString();
            // ReSharper restore ThrowExceptionInUnexpectedLocation
        }

        public ClarityEcommerceEntities()
            : base(DefaultConnectionString)
#else
        public ClarityEcommerceEntities()
            : base("Data Source=10.7.1.214;Initial Catalog =JBM_CEF_PRD_LIVE;User=abharati;Password=Support@123;Persist Security Info=True;MultipleActiveResultSets=true;")
#endif
        {
            Database.SetInitializer(new Migrations.ModelDBInitializer());
            Database.CommandTimeout = 10 * 60;
        }

        public ClarityEcommerceEntities(string name)
            : base(name)
        {
            Database.SetInitializer(new Migrations.ModelDBInitializer());
            Database.CommandTimeout = 10 * 60;
        }
#endif

        // ReSharper disable once FunctionComplexityOverflow
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
#if ORACLE
            modelBuilder.HasDefaultSchema("CLARITY");
#endif
            modelBuilder
                .Ignore<Base>()
                .Ignore<NameableBase>()
                .Ignore<DisplayableBase>()
                .Ignore<StateableBase>()
                .Ignore<StatusableBase>()
                .Ignore<TypableBase>()
                .Ignore<ImageBase>();
            modelBuilder.Ignore(
                new List<Type>
                {
                    typeof(SalesCollectionBase<,,,,,,,,,>),
                    typeof(SalesEventBase<,>),
                    typeof(SalesItemBase<,,,>),
                    typeof(SalesItemTargetBase<>),
                    typeof(AppliedDiscountBase<,>),
                });
            // Add conventions for Precision Attributes
#if ORACLE
            // Oracle can't do this
#else
            modelBuilder.Conventions.Add(new DateTimePrecisionAttributeConvention());
            base.OnModelCreating(modelBuilder);
            ImplementStringIsUnicodeAttributes(modelBuilder);
            ImplementDecimalPrecisionAttributes(modelBuilder);
#endif
            // Multiple Cascade Relationships duplicate in schema when you use Data Annotations, using fluent API prevents this
            modelBuilder.Entity<Note>().HasOptional(e => e.CreatedByUser).WithMany(e => e.NotesCreated).HasForeignKey(e => e.CreatedByUserID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Note>().HasOptional(e => e.UpdatedByUser).WithMany(e => e.NotesUpdated).HasForeignKey(e => e.UpdatedByUserID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Note>().HasOptional(e => e.User).WithMany(e => e.Notes).HasForeignKey(e => e.UserID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesInvoice>().HasOptional(e => e.ShippingContact).WithMany(e => e.ShippingContactsSalesInvoices).HasForeignKey(e => e.ShippingContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesInvoice>().HasOptional(e => e.BillingContact).WithMany(e => e.BillingContactsSalesInvoices).HasForeignKey(e => e.BillingContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesQuote>().HasOptional(e => e.ShippingContact).WithMany(e => e.ShippingContactsSalesQuotes).HasForeignKey(e => e.ShippingContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesQuote>().HasOptional(e => e.BillingContact).WithMany(e => e.BillingContactsSalesQuotes).HasForeignKey(e => e.BillingContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<PurchaseOrder>().HasOptional(e => e.ShippingContact).WithMany(e => e.ShippingContactsPurchaseOrders).HasForeignKey(e => e.ShippingContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<PurchaseOrder>().HasOptional(e => e.BillingContact).WithMany(e => e.BillingContactsPurchaseOrders).HasForeignKey(e => e.BillingContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesOrder>().HasOptional(e => e.BillingContact).WithMany(e => e.BillingContactsSalesOrders).HasForeignKey(e => e.BillingContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesOrder>().HasOptional(e => e.ShippingContact).WithMany(e => e.ShippingContactsSalesOrders).HasForeignKey(e => e.ShippingContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesReturn>().HasOptional(e => e.BillingContact).WithMany(e => e.BillingContactsSalesReturns).HasForeignKey(e => e.BillingContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesReturn>().HasOptional(e => e.ShippingContact).WithMany(e => e.ShippingContactsSalesReturns).HasForeignKey(e => e.ShippingContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductAssociation>().HasRequired(e => e.Master).WithMany(e => e.ProductAssociations).HasForeignKey(e => e.MasterID).WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductAssociation>().HasRequired(e => e.Slave).WithMany(e => e.ProductsAssociatedWith).HasForeignKey(e => e.SlaveID).WillCascadeOnDelete(false);
            modelBuilder.Entity<AccountAssociation>().HasRequired(e => e.Master).WithMany(e => e.AccountAssociations).HasForeignKey(e => e.MasterID).WillCascadeOnDelete(false);
            modelBuilder.Entity<AccountAssociation>().HasRequired(e => e.Slave).WithMany(e => e.AccountsAssociatedWith).HasForeignKey(e => e.SlaveID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(e => e.OriginContact).WithMany(e => e.OriginContactsShipments).HasForeignKey(e => e.OriginContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(e => e.DestinationContact).WithMany(e => e.DestinationContactsShipments).HasForeignKey(e => e.DestinationContactID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Review>().HasRequired(e => e.SubmittedByUser).WithMany(e => e.ReviewsSubmitted).HasForeignKey(e => e.SubmittedByUserID).WillCascadeOnDelete();
            modelBuilder.Entity<Review>().HasOptional(e => e.ApprovedByUser).WithMany(e => e.ReviewsApproved).HasForeignKey(e => e.ApprovedByUserID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Review>().HasOptional(e => e.User).WithMany(e => e.Reviews).HasForeignKey(e => e.UserID).WillCascadeOnDelete(false);
            modelBuilder.Entity<MessageAttachment>().HasRequired(e => e.CreatedByUser).WithMany(e => e.MessageAttachmentsCreated).HasForeignKey(e => e.CreatedByUserID).WillCascadeOnDelete(false);
            modelBuilder.Entity<MessageAttachment>().HasOptional(e => e.UpdatedByUser).WithMany(e => e.MessageAttachmentsUpdated).HasForeignKey(e => e.UpdatedByUserID).WillCascadeOnDelete(false);
            modelBuilder.Entity<HistoricalCurrencyRate>().HasRequired(e => e.StartingCurrency).WithMany(e => e.HistoricalStartingCurrencies).HasForeignKey(e => e.StartingCurrencyID).WillCascadeOnDelete();
            modelBuilder.Entity<HistoricalCurrencyRate>().HasRequired(e => e.EndingCurrency).WithMany(e => e.HistoricalEndingCurrencies).HasForeignKey(e => e.EndingCurrencyID).WillCascadeOnDelete(false);
            modelBuilder.Entity<CurrencyConversion>().HasRequired(e => e.StartingCurrency).WithMany(e => e.ConversionStartingCurrencies).HasForeignKey(e => e.StartingCurrencyID).WillCascadeOnDelete();
            modelBuilder.Entity<CurrencyConversion>().HasRequired(e => e.EndingCurrency).WithMany(e => e.ConversionEndingCurrencies).HasForeignKey(e => e.EndingCurrencyID).WillCascadeOnDelete(false);
            modelBuilder.Entity<UserEventAttendance>().HasRequired(e => e.Slave).WithMany(e => e.UserEventAttendances).WillCascadeOnDelete(false);
            modelBuilder.Entity<UserEventAttendance>().HasRequired(e => e.Master).WithMany(e => e.UserEventAttendances).WillCascadeOnDelete(false);
            modelBuilder.Entity<Brand>().HasOptional(e => e.MinimumOrderDollarAmountBufferCategory).WithMany(e => e.BrandMinimumOrderDollarAmountBufferCategories).HasForeignKey(e => e.MinimumOrderDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Brand>().HasOptional(e => e.MinimumOrderQuantityAmountBufferCategory).WithMany(e => e.BrandMinimumOrderQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumOrderQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Brand>().HasOptional(e => e.MinimumForFreeShippingDollarAmountBufferCategory).WithMany(e => e.BrandMinimumForFreeShippingDollarAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Brand>().HasOptional(e => e.MinimumForFreeShippingQuantityAmountBufferCategory).WithMany(e => e.BrandMinimumForFreeShippingQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Category>().HasOptional(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Category>().HasOptional(e => e.MinimumOrderDollarAmountBufferCategory).WithMany(e => e.MinimumOrderDollarAmountBufferCategories).HasForeignKey(e => e.MinimumOrderDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Category>().HasOptional(e => e.MinimumOrderQuantityAmountBufferCategory).WithMany(e => e.MinimumOrderQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumOrderQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Category>().HasOptional(e => e.MinimumForFreeShippingDollarAmountBufferCategory).WithMany(e => e.MinimumForFreeShippingDollarAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Category>().HasOptional(e => e.MinimumForFreeShippingQuantityAmountBufferCategory).WithMany(e => e.MinimumForFreeShippingQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Franchise>().HasOptional(e => e.MinimumOrderDollarAmountBufferCategory).WithMany(e => e.FranchiseMinimumOrderDollarAmountBufferCategories).HasForeignKey(e => e.MinimumOrderDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Franchise>().HasOptional(e => e.MinimumOrderQuantityAmountBufferCategory).WithMany(e => e.FranchiseMinimumOrderQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumOrderQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Franchise>().HasOptional(e => e.MinimumForFreeShippingDollarAmountBufferCategory).WithMany(e => e.FranchiseMinimumForFreeShippingDollarAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Franchise>().HasOptional(e => e.MinimumForFreeShippingQuantityAmountBufferCategory).WithMany(e => e.FranchiseMinimumForFreeShippingQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Manufacturer>().HasOptional(e => e.MinimumOrderDollarAmountBufferCategory).WithMany(e => e.ManufacturerMinimumOrderDollarAmountBufferCategories).HasForeignKey(e => e.MinimumOrderDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Manufacturer>().HasOptional(e => e.MinimumOrderQuantityAmountBufferCategory).WithMany(e => e.ManufacturerMinimumOrderQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumOrderQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Manufacturer>().HasOptional(e => e.MinimumForFreeShippingDollarAmountBufferCategory).WithMany(e => e.ManufacturerMinimumForFreeShippingDollarAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Manufacturer>().HasOptional(e => e.MinimumForFreeShippingQuantityAmountBufferCategory).WithMany(e => e.ManufacturerMinimumForFreeShippingQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Store>().HasOptional(e => e.MinimumOrderDollarAmountBufferCategory).WithMany(e => e.StoreMinimumOrderDollarAmountBufferCategories).HasForeignKey(e => e.MinimumOrderDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Store>().HasOptional(e => e.MinimumOrderQuantityAmountBufferCategory).WithMany(e => e.StoreMinimumOrderQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumOrderQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Store>().HasOptional(e => e.MinimumForFreeShippingDollarAmountBufferCategory).WithMany(e => e.StoreMinimumForFreeShippingDollarAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Store>().HasOptional(e => e.MinimumForFreeShippingQuantityAmountBufferCategory).WithMany(e => e.StoreMinimumForFreeShippingQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Vendor>().HasOptional(e => e.MinimumOrderDollarAmountBufferCategory).WithMany(e => e.VendorMinimumOrderDollarAmountBufferCategories).HasForeignKey(e => e.MinimumOrderDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Vendor>().HasOptional(e => e.MinimumOrderQuantityAmountBufferCategory).WithMany(e => e.VendorMinimumOrderQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumOrderQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Vendor>().HasOptional(e => e.MinimumForFreeShippingDollarAmountBufferCategory).WithMany(e => e.VendorMinimumForFreeShippingDollarAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingDollarAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Vendor>().HasOptional(e => e.MinimumForFreeShippingQuantityAmountBufferCategory).WithMany(e => e.VendorMinimumForFreeShippingQuantityAmountBufferCategories).HasForeignKey(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryID).WillCascadeOnDelete(false);
            modelBuilder.Entity<MembershipLevel>().HasRequired(e => e.Membership).WithMany(e => e.MembershipLevels).HasForeignKey(e => e.MembershipID).WillCascadeOnDelete(false);
            modelBuilder.Entity<MembershipRepeatType>().HasRequired(e => e.Master).WithMany(e => e.MembershipRepeatTypes).HasForeignKey(e => e.MasterID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SubscriptionTypeRepeatType>().HasRequired(e => e.Master).WithMany(e => e.SubscriptionTypeRepeatTypes).HasForeignKey(e => e.MasterID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Store>().HasRequired(e => e.Contact).WithMany(e => e.Stores).HasForeignKey(e => e.ContactID).WillCascadeOnDelete(false);
            // Sales Groups
            modelBuilder.Entity<SalesGroup>().HasMany(e => e.SalesQuoteResponseMasters).WithOptional(e => e.SalesGroupAsResponseMaster).HasForeignKey(e => e.SalesGroupAsResponseMasterID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesGroup>().HasMany(e => e.SalesQuoteResponseSubs).WithOptional(e => e.SalesGroupAsResponseSub).HasForeignKey(e => e.SalesGroupAsResponseSubID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesGroup>().HasMany(e => e.SalesQuoteRequestMasters).WithOptional(e => e.SalesGroupAsRequestMaster).HasForeignKey(e => e.SalesGroupAsRequestMasterID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesGroup>().HasMany(e => e.SalesQuoteRequestSubs).WithOptional(e => e.SalesGroupAsRequestSub).HasForeignKey(e => e.SalesGroupAsRequestSubID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesGroup>().HasMany(e => e.SalesOrderMasters).WithOptional(e => e.SalesGroupAsMaster).HasForeignKey(e => e.SalesGroupAsMasterID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesGroup>().HasMany(e => e.SubSalesOrders).WithOptional(e => e.SalesGroupAsSub).HasForeignKey(e => e.SalesGroupAsSubID).WillCascadeOnDelete(false);
            modelBuilder.Entity<SalesGroup>().HasMany(e => e.SampleRequests).WithOptional(e => e.SalesGroup).HasForeignKey(e => e.SalesGroupID).WillCascadeOnDelete(false);
            // Questionnaire
            modelBuilder.Entity<Question>().HasMany(e => e.FollowUpQuestionOptions).WithOptional(e => e.FollowUpQuestion).HasForeignKey(e => e.FollowUpQuestionID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Question>().HasMany(e => e.Options).WithRequired(e => e.Question).HasForeignKey(e => e.QuestionID).WillCascadeOnDelete(true);
            modelBuilder.Entity<Question>().HasMany(e => e.Answers).WithRequired(e => e.Question).HasForeignKey(e => e.QuestionID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Answer>().HasRequired(e => e.Question).WithMany(e => e.Answers).HasForeignKey(e => e.QuestionID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Answer>().HasRequired(e => e.Option).WithMany(e => e.Answers).WillCascadeOnDelete(false);
#if ORACLE
            // Oracle can't do this
#else
            // Schemas
            OnModelCreatingSchemas(modelBuilder);
            // ASP.NET Identity
            modelBuilder.Entity<UserLogin>().ToTable(nameof(UserLogin), "Contacts");
            modelBuilder.Entity<UserRole>().ToTable(nameof(UserRole), "Contacts");
            modelBuilder.Entity<UserClaim>().ToTable(nameof(UserClaim), "Contacts");
            modelBuilder.Entity<RoleUser>().ToTable(nameof(RoleUser), "Contacts");
            modelBuilder.Entity<User>().ToTable(nameof(User), "Contacts");
#endif
            modelBuilder.Entity<UserLogin>().HasRequired(e => e.User).WithMany(e => e.Logins).HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<UserClaim>().HasRequired(e => e.User).WithMany(e => e.Claims).HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<RoleUser>().HasRequired(e => e.User).WithMany(e => e.Roles).HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<RoleUser>().HasRequired(e => e.Role).WithMany(e => e.Users).HasForeignKey(e => e.RoleId).WillCascadeOnDelete(false);
            modelBuilder.Entity<UserRole>().HasKey(e => e.Id);
            modelBuilder.Entity<UserRole>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
#if ORACLE
            modelBuilder.Entity<RoleUser>().HasOptional(e => e.Group).WithMany().HasForeignKey(c => c.GroupID).WillCascadeOnDelete(false);
            modelBuilder.Entity<UserRole>().Property(e => e.Name).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(e => e.Email).HasMaxLength(256);
            // Another thing we must do is remove every specific data annotation that doesn't match Oracle's data
            // types. Oracle nuget package for EF6 can make necessary adjustments in general but if we made specific,
            // changes like we did with datetime2 using data annotations, then nuget package doesn't work out of the
            // box.
            // == Numerics for Taxes ==
            modelBuilder.Entity<TaxCountry>().Property(x => x.Rate).HasPrecision(7, 6);
            modelBuilder.Entity<TaxRegion>().Property(x => x.Rate).HasPrecision(7, 6);
            // == Byte arrays ==
            modelBuilder.Entity<ReportType>().Property(x => x.Template).HasColumnType("blob");
            // == Floats for Hangfire ==
            ////modelBuilder.Entity<HangfireSet>().Property(x => x.Score).HasColumnType("float");
#else
            // Dates
            OnModelCreatingDates(modelBuilder);
            // Numerics for Taxes
            modelBuilder.Entity<TaxCountry>().Property(x => x.Rate).HasColumnType("numeric").HasPrecision(7, 6);
            modelBuilder.Entity<TaxRegion>().Property(x => x.Rate).HasColumnType("numeric").HasPrecision(7, 6);
            // Byte arrays
            modelBuilder.Entity<ReportType>().Property(x => x.Template).HasColumnType("varbinary(max)");
            // Floats for Hangfire
            modelBuilder.Entity<HangfireSet>().Property(x => x.Score).HasColumnType("float");
#endif
        }

#if ORACLE
    // Oracle can't do this
#else
        private static void ImplementStringIsUnicodeAttributes(DbModelBuilder modelBuilder)
        {
            // Implement String Is Unicode Attributes
            foreach (var classType in Assembly.GetAssembly(typeof(StringIsUnicodeAttribute))
                                        .GetTypes()
                                        .Where(t => t.IsClass && !t.ContainsGenericParameters && t.Namespace == "Clarity.Ecommerce.DataModel"))
            {
                foreach (var propAttr in classType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                            .Where(p => p.GetCustomAttribute<StringIsUnicodeAttribute>() != null)
                                            .Select(p => new { prop = p, attr = p.GetCustomAttribute<StringIsUnicodeAttribute>(true) }))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var entityConfig = modelBuilder.GetType().GetMethod("Entity").MakeGenericMethod(classType).Invoke(modelBuilder, null);
                    var param = Expression.Parameter(classType, "c");
                    var property = Expression.Property(param, propAttr.prop.Name);
                    var lambdaExpression = Expression.Lambda(property, true, param);
                    var methodInfo = entityConfig.GetType()
                        .GetMethods()
                        .Where(p => p.Name == "Property")
                        .ToList()[5];
                    try
                    {
                        var stringConfig = methodInfo.Invoke(entityConfig, new object[] { lambdaExpression }) as StringPropertyConfiguration;
                        stringConfig?.IsUnicode(propAttr.attr.IsUnicode);
                    }
                    catch (Exception ex)
                    {
                        throw new($"Property '{propAttr.prop.Name}' has StringIsUnicode when it is the wrong type", ex);
                    }
                }
            }
        }

        private static void ImplementDecimalPrecisionAttributes(DbModelBuilder modelBuilder)
        {
            // Implement Decimal Precision Attributes
            foreach (var classType in Assembly.GetAssembly(typeof(DecimalPrecisionAttribute))
                        .GetTypes()
                        .Where(t => t.IsClass && !t.ContainsGenericParameters && t.Namespace == "Clarity.Ecommerce.DataModel"))
            {
                foreach (var propAttr in classType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(p => p.GetCustomAttribute<DecimalPrecisionAttribute>() != null)
                            .Select(p => new { prop = p, attr = p.GetCustomAttribute<DecimalPrecisionAttribute>(true) }))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var entityConfig = modelBuilder.GetType().GetMethod("Entity").MakeGenericMethod(classType).Invoke(modelBuilder, null);
                    var param = Expression.Parameter(classType, "c");
                    var property = Expression.Property(param, propAttr.prop.Name);
                    var lambdaExpression = Expression.Lambda(property, true, param);
                    DecimalPropertyConfiguration decimalConfig;
                    if (propAttr.prop.PropertyType.IsGenericType
                        && propAttr.prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[8];
                        decimalConfig = methodInfo.Invoke(entityConfig, new object[] { lambdaExpression }) as DecimalPropertyConfiguration;
                    }
                    else
                    {
                        var methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[7];
                        decimalConfig = methodInfo.Invoke(entityConfig, new object[] { lambdaExpression }) as DecimalPropertyConfiguration;
                    }
                    decimalConfig?.HasPrecision(propAttr.attr.Precision, propAttr.attr.Scale);
                }
            }
        }
#endif
    }
}
