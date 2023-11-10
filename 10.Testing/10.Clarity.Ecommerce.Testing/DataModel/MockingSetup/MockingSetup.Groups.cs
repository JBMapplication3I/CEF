// <autogenerated>
// <copyright file="Groups.MockingSetup.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Groups section of the Mocking Setup class</summary>
// <remarks>This file was auto-generated by MockingSetupMaster.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
// ReSharper disable CyclomaticComplexity, FunctionComplexityOverflow
#pragma warning disable CS0618 // Obsolete Items warnings ignored inside T4
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously ignored inside T4
#nullable enable
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        // Enable the entire schema on run
        public bool DoGroups { private get; set; }

        // Enable the tables specifically on run
        public bool DoGroupTable { private get; set; }
        public bool DoGroupStatusTable { private get; set; }
        public bool DoGroupTypeTable { private get; set; }
        public bool DoGroupUserTable { private get; set; }

        // Dirty Checking
        public bool GroupDirty { private get; set; }
        public bool GroupStatusDirty { private get; set; }
        public bool GroupTypeDirty { private get; set; }
        public bool GroupUserDirty { private get; set; }

        // Sets
        public Mock<DbSet<Group>>? Groups { get; private set; }
        public Mock<DbSet<GroupStatus>>? GroupStatuses { get; private set; }
        public Mock<DbSet<GroupType>>? GroupTypes { get; private set; }
        public Mock<DbSet<GroupUser>>? GroupUsers { get; private set; }

        // Raw Data
        public List<Mock<Group>>? RawGroups { get; private set; }
        public List<Mock<GroupStatus>>? RawGroupStatuses { get; private set; }
        public List<Mock<GroupType>>? RawGroupTypes { get; private set; }
        public List<Mock<GroupUser>>? RawGroupUsers { get; private set; }

        [System.Diagnostics.DebuggerStepThrough]
        public async Task AssignSchemaMocksGroupsAsync(Mock<IClarityEcommerceEntities> context, bool dirtyOnly)
        {
            if (DoAll || DoGroups || (DoGroupTable && (!dirtyOnly || GroupDirty)))
            {
                var attempts = 0;
                await RetryHelper.RetryOnExceptionAsync(async () =>
                {
                    if (++attempts > 1) { System.Diagnostics.Debug.WriteLine($"Assign Schema Mocks is taking extra attempts: {attempts}"); }
                    if (RawGroups == null) { throw new InvalidOperationException("Raw Groups was null"); }
                    await InitializeMockSetFromListAsync(Groups, RawGroups).ConfigureAwait(false);
                    context.Setup(m => m.Groups).Returns(() => Groups?.Object!);
                    context.Setup(m => m.Set<Group>()).Returns(() => Groups?.Object!);
                })
                .ConfigureAwait(false);
            }
            if (DoAll || DoGroups || (DoGroupStatusTable && (!dirtyOnly || GroupStatusDirty)))
            {
                var attempts = 0;
                await RetryHelper.RetryOnExceptionAsync(async () =>
                {
                    if (++attempts > 1) { System.Diagnostics.Debug.WriteLine($"Assign Schema Mocks is taking extra attempts: {attempts}"); }
                    if (RawGroupStatuses == null) { throw new InvalidOperationException("Raw GroupStatuses was null"); }
                    await InitializeMockSetFromListAsync(GroupStatuses, RawGroupStatuses).ConfigureAwait(false);
                    context.Setup(m => m.GroupStatuses).Returns(() => GroupStatuses?.Object!);
                    context.Setup(m => m.Set<GroupStatus>()).Returns(() => GroupStatuses?.Object!);
                })
                .ConfigureAwait(false);
            }
            if (DoAll || DoGroups || (DoGroupTypeTable && (!dirtyOnly || GroupTypeDirty)))
            {
                var attempts = 0;
                await RetryHelper.RetryOnExceptionAsync(async () =>
                {
                    if (++attempts > 1) { System.Diagnostics.Debug.WriteLine($"Assign Schema Mocks is taking extra attempts: {attempts}"); }
                    if (RawGroupTypes == null) { throw new InvalidOperationException("Raw GroupTypes was null"); }
                    await InitializeMockSetFromListAsync(GroupTypes, RawGroupTypes).ConfigureAwait(false);
                    context.Setup(m => m.GroupTypes).Returns(() => GroupTypes?.Object!);
                    context.Setup(m => m.Set<GroupType>()).Returns(() => GroupTypes?.Object!);
                })
                .ConfigureAwait(false);
            }
            if (DoAll || DoGroups || (DoGroupUserTable && (!dirtyOnly || GroupUserDirty)))
            {
                var attempts = 0;
                await RetryHelper.RetryOnExceptionAsync(async () =>
                {
                    if (++attempts > 1) { System.Diagnostics.Debug.WriteLine($"Assign Schema Mocks is taking extra attempts: {attempts}"); }
                    if (RawGroupUsers == null) { throw new InvalidOperationException("Raw GroupUsers was null"); }
                    await InitializeMockSetFromListAsync(GroupUsers, RawGroupUsers).ConfigureAwait(false);
                    context.Setup(m => m.GroupUsers).Returns(() => GroupUsers?.Object!);
                    context.Setup(m => m.Set<GroupUser>()).Returns(() => GroupUsers?.Object!);
                })
                .ConfigureAwait(false);
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        private async Task InitializeMockSetGroupsAsync(Mock<IClarityEcommerceEntities> context, List<Mock<Group>> data)
        {
            if (!DoAll && !DoGroups && !DoGroupTable) { return; }
            Groups ??= new Mock<DbSet<Group>>();
            await InitializeMockSetFromListAsync(Groups, data).ConfigureAwait(false);
            context.Setup(m => m.Groups).Returns(() => Groups.Object);
            context.Setup(m => m.Set<Group>()).Returns(() => Groups.Object);
            TableRecordCounts[typeof(Group)] = data.Count;
            if (data.Any())
            {
                TableFirstRecordIDs[typeof(Group)] = data[0].Object.ID;
                TableFirstRecordCustomKeys[typeof(Group)] = data[0].Object.CustomKey;
                TableFirstRecordNames[typeof(Group)] = data[0].Object.Name;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        private async Task InitializeMockSetGroupStatusesAsync(Mock<IClarityEcommerceEntities> context, List<Mock<GroupStatus>> data)
        {
            if (!DoAll && !DoGroups && !DoGroupStatusTable) { return; }
            GroupStatuses ??= new Mock<DbSet<GroupStatus>>();
            await InitializeMockSetFromListAsync(GroupStatuses, data).ConfigureAwait(false);
            context.Setup(m => m.GroupStatuses).Returns(() => GroupStatuses.Object);
            context.Setup(m => m.Set<GroupStatus>()).Returns(() => GroupStatuses.Object);
            TableRecordCounts[typeof(GroupStatus)] = data.Count;
            if (data.Any())
            {
                TableFirstRecordIDs[typeof(GroupStatus)] = data[0].Object.ID;
                TableFirstRecordCustomKeys[typeof(GroupStatus)] = data[0].Object.CustomKey;
                TableFirstRecordNames[typeof(GroupStatus)] = data[0].Object.Name;
                TableFirstRecordDisplayNames[typeof(GroupStatus)] = data[0].Object.DisplayName;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        private async Task InitializeMockSetGroupTypesAsync(Mock<IClarityEcommerceEntities> context, List<Mock<GroupType>> data)
        {
            if (!DoAll && !DoGroups && !DoGroupTypeTable) { return; }
            GroupTypes ??= new Mock<DbSet<GroupType>>();
            await InitializeMockSetFromListAsync(GroupTypes, data).ConfigureAwait(false);
            context.Setup(m => m.GroupTypes).Returns(() => GroupTypes.Object);
            context.Setup(m => m.Set<GroupType>()).Returns(() => GroupTypes.Object);
            TableRecordCounts[typeof(GroupType)] = data.Count;
            if (data.Any())
            {
                TableFirstRecordIDs[typeof(GroupType)] = data[0].Object.ID;
                TableFirstRecordCustomKeys[typeof(GroupType)] = data[0].Object.CustomKey;
                TableFirstRecordNames[typeof(GroupType)] = data[0].Object.Name;
                TableFirstRecordDisplayNames[typeof(GroupType)] = data[0].Object.DisplayName;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        private async Task InitializeMockSetGroupUsersAsync(Mock<IClarityEcommerceEntities> context, List<Mock<GroupUser>> data)
        {
            if (!DoAll && !DoGroups && !DoGroupUserTable) { return; }
            GroupUsers ??= new Mock<DbSet<GroupUser>>();
            await InitializeMockSetFromListAsync(GroupUsers, data).ConfigureAwait(false);
            context.Setup(m => m.GroupUsers).Returns(() => GroupUsers.Object);
            context.Setup(m => m.Set<GroupUser>()).Returns(() => GroupUsers.Object);
            TableRecordCounts[typeof(GroupUser)] = data.Count;
            if (data.Any())
            {
                TableFirstRecordIDs[typeof(GroupUser)] = data[0].Object.ID;
                TableFirstRecordCustomKeys[typeof(GroupUser)] = data[0].Object.CustomKey;
            }
        }

        /// <summary>Creates a dummy Group.</summary>
        /// <returns>The new Group.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        private Task<Mock<Group>> CreateADummyGroupAsync(int id, string? key, string? name, string? desc = null, int? parentID = null, int typeID = 1, int statusID = 1, string? jsonAttributes = null, int? groupOwnerID = null!, bool? active = null)
        {
            return Task.Run(async () =>
            {
                var mock = new Mock<Group>();
                mock.SetupAllProperties();
                // IBase Properties
                mock.Object.ID = id;
                mock.Object.CustomKey = key;
                mock.Object.Active = active.HasValue ? active.Value : !DoInactives;
                mock.Object.CreatedDate = CreatedDate;
                mock.Object.UpdatedDate = null;
                mock.Object.Hash = null;
                mock.Object.JsonAttributes = jsonAttributes;
                // INameableBase Properties
                mock.Object.Name = name;
                mock.Object.Description = desc;
                // IHaveATypeBase Properties
                mock.Object.TypeID = typeID;
                // IHaveAStatusBase Properties
                mock.Object.StatusID = statusID;
                // IHaveAParentBase Properties
                mock.Object.ParentID = parentID;
                // Group Properties
                mock.Object.GroupOwnerID = groupOwnerID;
                // Dynamic Linking
                // IHaveATypeBase Properties
                mock.Setup(m => m.Type).Returns(() => RawGroupTypes?.FirstOrDefault(x => x.Object.ID == mock.Object.TypeID)?.Object);
                // IHaveAStatusBase Properties
                mock.Setup(m => m.Status).Returns(() => RawGroupStatuses?.FirstOrDefault(x => x.Object.ID == mock.Object.StatusID)?.Object);
                // IHaveAParentBase Properties
                mock.Setup(m => m.Parent).Returns(() => RawGroups == null || mock.Object.ParentID == null ? null : RawGroups.FirstOrDefault(x => x.Object.ID == mock.Object.ParentID)?.Object);
                mock.Setup(m => m.Children).Returns(() => RawGroups?.Where(x => x.Object.ParentID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<Group>());
                // Group's Related Objects
                mock.Setup(m => m.GroupOwner).Returns(() => RawUsers?.FirstOrDefault(x => x.Object.ID == mock.Object.GroupOwnerID)?.Object);
                // Group's Associated Objects
                mock.Setup(m => m.Users).Returns(() => RawGroupUsers?.Where(x => x.Object.MasterID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<GroupUser>());
                return mock;
            });
        }

        /// <summary>Creates a dummy GroupStatus.</summary>
        /// <returns>The new GroupStatus.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        private Task<Mock<GroupStatus>> CreateADummyGroupStatusAsync(int id, string? key, string? name, string? desc = null, int? sortOrder = null, string? displayName = null, string? translationKey = null, string? jsonAttributes = null, bool? active = null)
        {
            return Task.Run(async () =>
            {
                var mock = new Mock<GroupStatus>();
                mock.SetupAllProperties();
                // IBase Properties
                mock.Object.ID = id;
                mock.Object.CustomKey = key;
                mock.Object.Active = active.HasValue ? active.Value : !DoInactives;
                mock.Object.CreatedDate = CreatedDate;
                mock.Object.UpdatedDate = null;
                mock.Object.Hash = null;
                mock.Object.JsonAttributes = jsonAttributes;
                // INameableBase Properties
                mock.Object.Name = name;
                mock.Object.Description = desc;
                // IDisplayableBase Properties
                mock.Object.SortOrder = sortOrder;
                mock.Object.DisplayName = displayName;
                mock.Object.TranslationKey = translationKey;
                // GroupStatus Properties
                // Dynamic Linking
                // GroupStatus's Related Objects
                // GroupStatus's Associated Objects
                return mock;
            });
        }

        /// <summary>Creates a dummy GroupType.</summary>
        /// <returns>The new GroupType.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        private Task<Mock<GroupType>> CreateADummyGroupTypeAsync(int id, string? key, string? name, string? desc = null, int? sortOrder = null, string? displayName = null, string? translationKey = null, string? jsonAttributes = null, bool? active = null)
        {
            return Task.Run(async () =>
            {
                var mock = new Mock<GroupType>();
                mock.SetupAllProperties();
                // IBase Properties
                mock.Object.ID = id;
                mock.Object.CustomKey = key;
                mock.Object.Active = active.HasValue ? active.Value : !DoInactives;
                mock.Object.CreatedDate = CreatedDate;
                mock.Object.UpdatedDate = null;
                mock.Object.Hash = null;
                mock.Object.JsonAttributes = jsonAttributes;
                // INameableBase Properties
                mock.Object.Name = name;
                mock.Object.Description = desc;
                // IDisplayableBase Properties
                mock.Object.SortOrder = sortOrder;
                mock.Object.DisplayName = displayName;
                mock.Object.TranslationKey = translationKey;
                // GroupType Properties
                // Dynamic Linking
                // GroupType's Related Objects
                // GroupType's Associated Objects
                return mock;
            });
        }

        /// <summary>Creates a dummy GroupUser.</summary>
        /// <returns>The new GroupUser.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        private Task<Mock<GroupUser>> CreateADummyGroupUserAsync(int id, string? key, string? jsonAttributes = null, int masterID = 1, int slaveID = 1, bool? active = null)
        {
            return Task.Run(async () =>
            {
                var mock = new Mock<GroupUser>();
                mock.SetupAllProperties();
                // IBase Properties
                mock.Object.ID = id;
                mock.Object.CustomKey = key;
                mock.Object.Active = active.HasValue ? active.Value : !DoInactives;
                mock.Object.CreatedDate = CreatedDate;
                mock.Object.UpdatedDate = null;
                mock.Object.Hash = null;
                mock.Object.JsonAttributes = jsonAttributes;
                // GroupUser Properties
                mock.Object.MasterID = masterID;
                mock.Object.SlaveID = slaveID;
                // Dynamic Linking
                // GroupUser's Related Objects
                mock.Setup(m => m.Master).Returns(() => RawGroups?.FirstOrDefault(x => x.Object.ID == mock.Object.MasterID)?.Object);
                mock.Setup(m => m.Slave).Returns(() => RawUsers?.FirstOrDefault(x => x.Object.ID == mock.Object.SlaveID)?.Object);
                // GroupUser's Associated Objects
                return mock;
            });
        }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Moq;

    internal partial class DataModelTestingRegistry
    {
        private void CreateRegistryForGroups(MockingSetup mockingSetup)
        {
            Func<Group> mockFuncGroup = () =>
            {
                var mock = new Mock<Group>();
                mock.SetupAllProperties();
                mock.Setup(m => m.ToHashableString()).CallBase();
                // Group Properties
                // Dynamic Linking
                // IHaveATypeBase Properties
                mock.Setup(m => m.Type).Returns(() => mockingSetup.RawGroupTypes?.FirstOrDefault(x => x.Object.ID == mock.Object.TypeID)?.Object);
                // IHaveAStatusBase Properties
                mock.Setup(m => m.Status).Returns(() => mockingSetup.RawGroupStatuses?.FirstOrDefault(x => x.Object.ID == mock.Object.StatusID)?.Object);
                // IHaveAParentBase Properties
                mock.Setup(m => m.Parent).Returns(() => mockingSetup.RawGroups == null || mock.Object.ParentID == null ? null : mockingSetup.RawGroups.FirstOrDefault(x => x.Object.ID == mock.Object.ParentID)?.Object);
                mock.Setup(m => m.Children).Returns(() => mockingSetup.RawGroups?.Where(x => x.Object.ParentID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<Group>());
                mock.Setup(m => m.GroupOwner).Returns(() => mockingSetup.RawUsers?.FirstOrDefault(x => x.Object.ID == mock.Object.GroupOwnerID)?.Object);
                // Group's Associated Objects
                mock.Setup(m => m.Users).Returns(() => mockingSetup.RawGroupUsers?.Where(x => x.Object.MasterID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<GroupUser>());
                // Group's Related Objects
                return mock.Object;
            };
            For<IGroup>().Use(() => mockFuncGroup());
            For<Group>().Use(() => mockFuncGroup());
            Func<GroupStatus> mockFuncGroupStatus = () =>
            {
                var mock = new Mock<GroupStatus>();
                mock.SetupAllProperties();
                mock.Setup(m => m.ToHashableString()).CallBase();
                // GroupStatus Properties
                // Dynamic Linking
                // GroupStatus's Associated Objects
                // GroupStatus's Related Objects
                return mock.Object;
            };
            For<IGroupStatus>().Use(() => mockFuncGroupStatus());
            For<GroupStatus>().Use(() => mockFuncGroupStatus());
            Func<GroupType> mockFuncGroupType = () =>
            {
                var mock = new Mock<GroupType>();
                mock.SetupAllProperties();
                mock.Setup(m => m.ToHashableString()).CallBase();
                // GroupType Properties
                // Dynamic Linking
                // GroupType's Associated Objects
                // GroupType's Related Objects
                return mock.Object;
            };
            For<IGroupType>().Use(() => mockFuncGroupType());
            For<GroupType>().Use(() => mockFuncGroupType());
            Func<GroupUser> mockFuncGroupUser = () =>
            {
                var mock = new Mock<GroupUser>();
                mock.SetupAllProperties();
                mock.Setup(m => m.ToHashableString()).CallBase();
                // GroupUser Properties
                // Dynamic Linking
                mock.Setup(m => m.Master).Returns(() => mockingSetup.RawGroups?.FirstOrDefault(x => x.Object.ID == mock.Object.MasterID)?.Object);
                mock.Setup(m => m.Slave).Returns(() => mockingSetup.RawUsers?.FirstOrDefault(x => x.Object.ID == mock.Object.SlaveID)?.Object);
                // GroupUser's Associated Objects
                // GroupUser's Related Objects
                return mock.Object;
            };
            For<IGroupUser>().Use(() => mockFuncGroupUser());
            For<GroupUser>().Use(() => mockFuncGroupUser());
        }
    }
}
