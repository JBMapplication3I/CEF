// <copyright file="SourcePropertyTreeAggregate.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the source property tree aggregate class</summary>
#pragma warning disable SA1600 // Elements should be documented
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Interfaces.DataModel;
    using LinqKit;

    /// <summary>An SPTA.</summary>
    internal class SPTA
    {
        private string fullKey;

        /// <summary>Set up the Tree nature.</summary>
        /// <param name="levelKey">                 The level key.</param>
        /// <param name="lambdaNameInstanceCounter">The lambda name instance counter.</param>
        /// <param name="parent">                   The parent.</param>
        /// <param name="isPropertyOfParent">       True if this SPTA is property of parent.</param>
        /// <param name="isJsonAttributeOf">        True if this SPTA is JSON attribute of.</param>
        internal SPTA(string levelKey, ref int lambdaNameInstanceCounter, SPTA parent, bool isPropertyOfParent = false, bool isJsonAttributeOf = false)
        {
            LevelKey = levelKey;
            Parent = parent;
            if (!isPropertyOfParent && !isJsonAttributeOf)
            {
                LevelSourceType = DataModelAssembly.GetType($"Clarity.Ecommerce.DataModel.{LevelKey}", true, false);
                lambdaNameInstanceCounter++;
                var letter = ExcelSalesQuoteImporterProvider.GenColumnReference(lambdaNameInstanceCounter);
                LevelParameterExpression = Expression.Parameter(LevelSourceType, "level" + letter);
                LevelParameterExpression.Expand();
            }
            else if (isPropertyOfParent)
            {
                if (Parent.LevelSourceCollectionType != null)
                {
                    LevelProperty = Parent.LevelSourceCollectionType?.GetProperty(LevelKey);
                }
                else if (Parent.LevelSourceType != null)
                {
                    LevelProperty = Parent.LevelSourceType.GetProperty(LevelKey);
                }
                // ReSharper disable once PossibleNullReferenceException
                LevelSourceType = LevelProperty.PropertyType;
                if (LevelSourceType.Name == typeof(ICollection<>).Name)
                {
                    LevelSourceCollectionType = LevelSourceType.GenericTypeArguments[0];
                    var (firstOrDefaultTMethod, firstOrDefaultClause) = FirstCollectionMemberThatIsActiveExpression(LevelSourceCollectionType, ref lambdaNameInstanceCounter);
                    LevelPropertyExpression = Expression.PropertyOrField(Parent.LevelParameterExpression ?? Parent.LevelPropertyExpression, LevelKey);
                    LevelCollectionSelectFirstActiveExpression = Expression.Call(firstOrDefaultTMethod, LevelPropertyExpression, firstOrDefaultClause);
                }
                else if (Parent.LevelSourceCollectionType != null)
                {
                    LevelPropertyExpression = Expression.PropertyOrField(Parent.LevelCollectionSelectFirstActiveExpression, LevelKey);
                }
                else
                {
                    LevelPropertyExpression = Expression.PropertyOrField(Parent.LevelParameterExpression ?? Parent.LevelPropertyExpression, LevelKey);
                }
            }
            else
            {
                // Always true: isJsonAttributeOf
                LevelSourceType = typeof(string);
                // We're just forwarding it through for ease of access
                LevelJsonAttributesProperty = Parent.LevelJsonAttributesProperty;
                if (Parent.LevelSourceCollectionType != null)
                {
                    if (LevelKey == "JsonAttributes")
                    {
                        LevelPropertyExpression = Expression.PropertyOrField(Parent.LevelCollectionSelectFirstActiveExpression, LevelKey);
                        LevelJsonAttributesPropertyExpression = LevelPropertyExpression; // Proved correct
                    }
                }
                else if (Parent.IsJsonChild)
                {
                    if (Parent.LevelKey == "JsonAttributes")
                    {
                        if (Parent.LevelSourceType == typeof(string))
                        {
                            // "Manu_Pkging" says Parent.LevelPropertyExpression is "JsonAttributes"
                            LevelPropertyExpression = Parent.LevelParameterExpression ?? Parent.LevelPropertyExpression;
                        }
                    }
                    LevelJsonAttributesPropertyExpression = LevelPropertyExpression;
                }
                else
                {
                    LevelJsonAttributesPropertyExpression = Expression.PropertyOrField(Parent.LevelParameterExpression ?? Parent.LevelPropertyExpression, "JsonAttributes");
                    LevelPropertyExpression = LevelJsonAttributesPropertyExpression;
                }
                IsJsonChild = true;
            }
            if (LevelKey == "JsonAttributes")
            {
                LevelJsonAttributesPropertyExpression = LevelPropertyExpression
                    ?? throw new Exception("Something went wrong");
            }
            Children = new Dictionary<string, SPTA>();
        }

        internal bool HasChildren => Children.Count > 0;

        internal bool IsJsonChild { get; }

        internal PropertyInfo LevelProperty { get; }

        internal Type LevelDynamicType { get; private set; }

        private static Assembly DataModelAssembly { get; } = Assembly.GetAssembly(typeof(IClarityEcommerceEntities));

        private SPTA Parent { get; }

        private Dictionary<string, SPTA> Children { get; }

        private bool HasJsonChildren => Children.Any(x => x.Value.IsJsonChild);

        // Properties to store for this level
        private Expression LevelParameterExpression { get; }

        private Expression LevelPropertyExpression { get; }

        private Expression LevelCollectionSelectFirstActiveExpression { get; }

        private Expression LevelJsonAttributesPropertyExpression { get; }

        private string LevelKey { get; }

        private PropertyInfo LevelJsonAttributesProperty { get; }

        private Type LevelSourceType { get; }

        private Type LevelSourceCollectionType { get; }

        private Type LevelResultType { get; set; }

        private List<MemberBinding> Bindings { get; set; }

        private ParameterExpression SourceItemExpression { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private Expression SelectExpression { get; set; }

        internal SPTA this[string key] { get => Children[key]; set => Children[key] = value; }

        internal bool ContainsKey(string key)
        {
            return Children.ContainsKey(key);
        }

        internal string GetFullKey()
        {
            if (fullKey != null)
            {
                return fullKey;
            } // Cache it for reuse
            var parent = Parent;
            fullKey = LevelKey;
            while (parent != null)
            {
                fullKey = $"{parent.LevelKey}.{fullKey}";
                parent = parent.Parent;
            }
            return fullKey;
        }

        internal void GeneratePreCall(
            Dictionary<string, SPTA> sourceProps,
            ref int lambdaNameInstanceCounter,
            string? contextProfileName)
        {
            var dict = GenerateDictNoJsonAttrs();
            if (dict.Count == 0)
            {
                LevelResultType = typeof(string);
                return;
            }
            LevelDynamicType = LinqRuntimeTypeBuilder.GetDynamicType(GenerateDictNoJsonAttrs(), contextProfileName);
            LevelResultType = LevelSourceType;
            if (!HasChildren || (LevelSourceCollectionType ?? LevelResultType) == typeof(string))
            {
                return;
            }
            GenerateMemberBindings(sourceProps);
            GenerateSourceItemExpression(ref lambdaNameInstanceCounter);
            GenerateSelectExpression();
        }

        private static (MethodInfo method, Expression clause) FirstCollectionMemberThatIsActiveExpression(
            Type type,
            ref int lambdaNameInstanceCounter)
        {
            lambdaNameInstanceCounter++;
            var letter = ExcelSalesQuoteImporterProvider.GenColumnReference(lambdaNameInstanceCounter);
            var targetExp = Expression.Parameter(type, "firstOf" + letter);
            var fieldExp = Expression.Property(targetExp, "Active");
            var valueExp = Expression.Constant(true);
            var matchExp = Expression.Equal(fieldExp, valueExp);
            // TODO: Filter by "Instance"?
            /*var valueExp2 = Expression.Constant(1765116);
            var fieldExp2 = Expression.Property(targetExp, "id");
            var assignExp2 = Expression.Equal(fieldExp2, valueExp2);
            var predicateBody1 = Expression.AndAlso(matchExp, assignExp2);*/
            var firstOrDefaultMethod = typeof(Enumerable/*Queryable*/).GetMethods()
                .First(m => m.Name == "FirstOrDefault"
                         && m.IsGenericMethodDefinition && m.GetParameters().Length == 2);
            var funcType = typeof(Func<,>).MakeGenericType(type, typeof(bool));
            var lambdaMethod = typeof(Expression).GetMethods()
                .Single(x => x.Name == "Lambda"
                          && x.GetGenericArguments().Length == 1
                          && x.GetParameters().Length == 2
                          && x.GetParameters()[1].ParameterType.IsArray);
            var lambdaMethodT = lambdaMethod.MakeGenericMethod(funcType);
            var firstOrDefaultClause = (Expression)lambdaMethodT.Invoke(null, new object[] { matchExp/*predicateBody1*/, new[] { targetExp } });
            var firstOrDefaultTMethod = firstOrDefaultMethod.MakeGenericMethod(type);
            return (firstOrDefaultTMethod, firstOrDefaultClause);
        }

        private Dictionary<string, SPTA> GenerateDictNoJsonAttrs()
        {
            // Get the non-JSON Children
            var dict = Children
                .Where(x => x.Value.LevelProperty != null && !x.Value.IsJsonChild)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            // ReSharper disable once InvertIf
            if (HasJsonChildren && Children.Any(x => x.Value.LevelKey == "JsonAttributes"))
            {
                var json = Children.Single(x => x.Value.LevelKey == "JsonAttributes").Value;
                if (json != null)
                {
                    dict["JsonAttributes"] = json;
                }
            }
            return dict;
        }

        private void GenerateMemberBindings(IReadOnlyDictionary<string, SPTA> sourceProps)
        {
            var list = new List<MemberBinding>();
            foreach (var field in LevelDynamicType.GetFields())
            {
                var keys = field.GetCustomAttribute<DescriptionAttribute>().Description.Split('.');
                var result = sourceProps[keys[0]];
                foreach (var key in keys.Skip(1))
                {
                    result = result[key];
                }
                Expression expr;
                if (result.Children?.ContainsKey(field.Name.Replace("_", string.Empty)) == true)
                {
                    expr = result.Children[field.Name.Replace("_", string.Empty)].LevelPropertyExpression;
                }
                else
                {
                    expr = result.LevelParameterExpression ?? result.LevelPropertyExpression;
                }
                var assignment = Expression.Bind(field, expr);
                if (assignment is MemberBinding binding)
                {
                    list.Add(binding);
                }
            }
            Bindings = list;
        }

        private void GenerateSourceItemExpression(ref int lambdaNameInstanceCounter)
        {
            lambdaNameInstanceCounter++;
            var letter = ExcelSalesQuoteImporterProvider.GenColumnReference(lambdaNameInstanceCounter);
            SourceItemExpression = Expression.Parameter(LevelSourceType, "sourceItem" + letter);
        }

        private void GenerateSelectExpression()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            SelectExpression = Expression.Lambda(
                Expression.MemberInit(
                        Expression.New(LevelDynamicType.GetConstructor(Type.EmptyTypes)),
                        Bindings)
                    .Expand(),
                SourceItemExpression);
        }
    }
}
