// <copyright file="DatabaseDescriptor.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the database descriptor class</summary>
// <remarks>This class was discovered at:
// http://www.entityframework.info/Home/MetadataValidation
// </remarks>
// ReSharper disable NotAccessedField.Global, UnusedMember.Global
namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Core.Objects.DataClasses;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    public sealed class DatabaseDescriptor
    {
        private static readonly object ByTypeLock = new();

        private static readonly Dictionary<Type, DatabaseDescriptor> DescriptorByType = new();

        private Dictionary<string, Dictionary<string, ColumnDesc>> fieldDescriptors = null!;

        private DatabaseDescriptor(ObjectContext db)
        {
            InitializeMetadata(db);
            DescriptorByType[db.GetType()] = this;
        }

        public static PropertyInfo[] GetColumns(Type entityType)
        {
            var columns = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (IsEntity(entityType))
            {
                columns = columns.Where(IsRealColumn).ToArray();
            }
            return columns;
        }

        public static DatabaseDescriptor Get(DbContext db)
        {
            var objContext = ((IObjectContextAdapter)db).ObjectContext;
            return Get(objContext);
        }

        public static void DateTimeZeroCheck(ObjectContext db)
        {
            var modified = db.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified);
            foreach (var entry in modified.Where(entry => !entry.IsRelationship))
            {
                var entity = entry.Entity;
                Debug.Assert(entity != null, "entity != null");
                var type = entity!.GetType();
                foreach (var prop in type.GetProperties().Where(p => p.PropertyType == typeof(DateTime)))
                {
                    var value = (DateTime)prop.GetValue(entity, null)!;
                    if (value == DateTime.MinValue)
                    {
                        throw new($"Datetime2 is 0 Table {type} Column {prop.Name}");
                    }
                }
                foreach (var prop in type.GetProperties().Where(
                    p => p.PropertyType.IsGenericType &&
                        p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                            p.PropertyType.GetGenericArguments()[0] == typeof(DateTime)))
                {
                    var value = (DateTime?)prop.GetValue(entity, null);
                    if (value == DateTime.MinValue)
                    {
                        throw new($"Datetime2 is 0 Table {type} Column {prop.Name}");
                    }
                }
            }
        }

        public ColumnDesc[]? DbInfo(Type t)
        {
            return fieldDescriptors.TryGetValue(t.Name, out var entity) ? entity.Values.ToArray() : null;
        }

        public ColumnDesc DbInfo(PropertyInfo member)
        {
            if (member.ReflectedType != null
                && fieldDescriptors.TryGetValue(member.ReflectedType.Name, out var entity)
                && entity.TryGetValue(member.Name, out var result))
            {
                return result;
            }
            throw new($"Member not found: {member.ReflectedType!.Name}.{member.Name}");
        }

        public void StringOverflowCheck(ObjectContext db)
        {
            var modified = db.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified);
            foreach (var entry in modified.Where(entry => !entry.IsRelationship))
            {
                var entity = entry.Entity;
                Debug.Assert(entity != null, "entity != null");
                var type = entity!.GetType();
                if (type.BaseType != null && type.Namespace == "System.Data.Entity.DynamicProxies")
                {
                    type = type.BaseType;
                }
                var fieldMap = fieldDescriptors[type.Name];
                foreach (var key in fieldMap.Keys)
                {
                    if (fieldMap[key].MaxLength <= 0)
                    {
                        continue;
                    }
                    var value = (string?)type.GetProperty(key)!.GetValue(entity, null);
                    if (value != null && value.Length > fieldMap[key].MaxLength)
                    {
                        throw new(
                            $"String Overflow on Table {type} Column {key}: {value.Length} out of {fieldMap[key].MaxLength} with value'{value}' ");
                    }
                }
            }
        }

        private static DatabaseDescriptor Get(ObjectContext db)
        {
            DatabaseDescriptor? result;
            lock (ByTypeLock)
            {
                var type = db.GetType();
                if (DescriptorByType.TryGetValue(type, out result))
                {
                    return result!;
                }
                result = new(db);
                DescriptorByType[type] = result;
            }
            return result!;
        }

        private static bool IsEntity(Type propertyType)
        {
            return !IsSimpleType(propertyType) && !propertyType.IsGenericType;
        }

        private static bool IsSimpleType(Type propertyType)
        {
            return propertyType.IsPrimitive || propertyType.IsValueType || propertyType == typeof(string) || propertyType == typeof(decimal) || propertyType == typeof(DateTime) || propertyType == typeof(byte[]);
        }

        private static bool IsRealColumn(PropertyInfo prop)
        {
            var propertyType = prop.PropertyType;
            return prop.CanWrite
                && !IsEntity(propertyType)
                && !propertyType.IsSubclassOf(typeof(EntityReference))
                && !propertyType.IsSubclassOf(typeof(RelatedEnd))
                && (!propertyType.IsGenericType || propertyType.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        private void InitializeMetadata(ObjectContext ctx)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (fieldDescriptors != null)
            {
                return;
            }
            fieldDescriptors = new();
            var items = ctx.MetadataWorkspace.GetItems(DataSpace.CSpace);
            Debug.Assert(items != null, "items != null");
            var tables = items!.Where(
                m => m.BuiltInTypeKind is BuiltInTypeKind.EntityType or BuiltInTypeKind.ComplexType);
            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (StructuralType table in tables)
            {
                var fieldsMap = new Dictionary<string, ColumnDesc>();
                fieldDescriptors[table.Name] = fieldsMap;
                ReadOnlyMetadataCollection<EdmProperty>? props = null;
                if (table is ComplexType complexType)
                {
                    props = complexType.Properties;
                }
                if (table is EntityType entityType)
                {
                    props = entityType.Properties;
                }
                Debug.Assert(props != null, "props != null");
                foreach (var field in props!.Where(p => p.DeclaringType.Name == table.Name))
                {
                    var maxLength = 0;
                    byte scale = 0;
                    switch (field.TypeUsage.EdmType.Name)
                    {
                        case "String":
                        {
                            var value = field.TypeUsage.Facets["MaxLength"].Value;
                            maxLength = value is int ? Convert.ToInt32(value) : int.MaxValue;
                            break;
                        }
                        case "Decimal":
                        {
                            var value = field.TypeUsage.Facets["Scale"].Value;
                            scale = Convert.ToByte(value);
                            break;
                        }
                    }
                    fieldsMap[field.Name] = new(field.Name, field.Nullable, maxLength, scale);
                }
            }
        }
    }
}
