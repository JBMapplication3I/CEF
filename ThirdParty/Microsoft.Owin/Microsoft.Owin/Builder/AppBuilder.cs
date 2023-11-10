// <copyright file="AppBuilder.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the application builder class</summary>
namespace Microsoft.Owin.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using Infrastructure;

    /// <summary>A standard implementation of IAppBuilder.</summary>
    /// <seealso cref="IAppBuilder"/>
    public class AppBuilder : global::Owin.IAppBuilder
    {
        /// <summary>The not found.</summary>
        private static readonly Func<IDictionary<string, object>, Task> NotFound;

        /// <summary>The conversions.</summary>
        private readonly IDictionary<Tuple<Type, Type>, Delegate> _conversions;

        /// <summary>The middleware.</summary>
        private readonly IList<Tuple<Type, Delegate, object[]>> _middleware;

        /// <summary>Initializes static members of the Microsoft.Owin.Builder.AppBuilder class.</summary>
        static AppBuilder()
        {
            NotFound = new NotFound().Invoke;
        }

        /// <summary>Initializes a new instance of the the type.</summary>
        public AppBuilder()
        {
            Properties = new Dictionary<string, object>();
            _conversions = new Dictionary<Tuple<Type, Type>, Delegate>();
            _middleware = new List<Tuple<Type, Delegate, object[]>>();
            Properties["builder.AddSignatureConversion"] = new Action<Delegate>(AddSignatureConversion);
            Properties["builder.DefaultApp"] = NotFound;
            SignatureConversions.AddConversions(this);
        }

        /// <summary>Initializes a new instance of the <see cref="AppBuilder" /> class.</summary>
        /// <param name="conversions">.</param>
        /// <param name="properties"> .</param>
        internal AppBuilder(
            IDictionary<Tuple<Type, Type>, Delegate> conversions,
            IDictionary<string, object> properties)
        {
            Properties = properties;
            _conversions = conversions;
            _middleware = new List<Tuple<Type, Delegate, object[]>>();
        }

        /// <summary>Contains arbitrary properties which may added, examined, and modified by components during the
        /// startup sequence.</summary>
        /// <value>Returns <see cref="System.Collections.Generic.IDictionary`2" />.</value>
        public IDictionary<string, object> Properties { get; }

        /// <summary>The Build is called at the point when all of the middleware should be chained together. This is
        /// typically done by the hosting component which created the app builder, and does not need to be called by the
        /// startup method if the IAppBuilder is passed in.</summary>
        /// <param name="returnType">The Type argument indicates which calling convention should be returned, and is
        ///                          typically
        ///                          typeof(<typeref name="Func&lt;IDictionary&lt;string,object&gt;, Task&gt;" />)
        ///                          for the OWIN calling convention.</param>
        /// <returns>Returns an instance of the pipeline's entry point. This object may be safely cast to the type which
        /// was provided.</returns>
        public object Build(Type returnType)
        {
            return BuildInternal(returnType);
        }

        /// <summary>The New method creates a new instance of an IAppBuilder. This is needed to create a tree structure
        /// in your processing, rather than a linear pipeline. The new instance share the same Properties, but will be
        /// created with a new, empty middleware list. To create a tangent pipeline you would first call New, followed
        /// by several calls to Use on the new builder, ending with a call to Build on the new builder. The return value
        /// from Build will be the entry-point to your tangent pipeline. This entry-point may now be added to the main
        /// pipeline as an argument to a switching middleware, which will either call the tangent pipeline or the "next
        /// app", based on something in the request. That said - all of that work is typically hidden by a middleware
        /// like Map, which will do that for you.</summary>
        /// <returns>The new instance of the IAppBuilder implementation.</returns>
        public global::Owin.IAppBuilder New()
        {
            return new AppBuilder(_conversions, Properties);
        }

        /// <summary>Adds a middleware node to the OWIN function pipeline. The middleware are invoked in the order they
        /// are added: the first middleware passed to Use will be the outermost function, and the last middleware passed
        /// to Use will be the innermost.</summary>
        /// <param name="middleware">The middleware parameter determines which behavior is being chained into the
        ///                          pipeline. If the middleware given to Use is a Delegate, then it will be invoked
        ///                          with the "next app" in the chain as the first parameter. If the delegate takes
        ///                          more than the single argument, then the additional values must be provided to
        ///                          Use in the args array. If the middleware given to Use is a Type, then the public
        ///                          constructor will be invoked with the "next app" in the chain as the first
        ///                          parameter. The resulting object must have a public Invoke method. If the object
        ///                          has constructors which take more than the single "next app" argument, then
        ///                          additional values may be provided in the args array.</param>
        /// <param name="args">      Any additional args passed to Use will be passed as additional values, following the
        ///                          "next app" parameter, when the OWIN call pipeline is build. They are passed as
        ///                          additional parameters if the middleware parameter is a Delegate, or as
        ///                          additional constructor arguments if the middle parameter is a Type.</param>
        /// <returns>The IAppBuilder itself is returned. This enables you to chain your use statements together.</returns>
        public global::Owin.IAppBuilder Use(object middleware, params object[] args)
        {
            _middleware.Add(ToMiddlewareFactory(middleware, args));
            return this;
        }

        /// <summary>Gets parameter type.</summary>
        /// <param name="function">The function.</param>
        /// <returns>The parameter type.</returns>
        private static Type GetParameterType(Delegate function)
        {
            var parameters = function.Method.GetParameters();
            if (parameters.Length < 1)
            {
                return null;
            }
            return parameters[0].ParameterType;
        }

        /// <summary>Tests argument for parameter.</summary>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <param name="arg">          The argument.</param>
        /// <returns>True if the test passes, false if the test fails.</returns>
        private static bool TestArgForParameter(Type parameterType, object arg)
        {
            if (arg == null && !parameterType.IsValueType)
            {
                return true;
            }
            return parameterType.IsInstanceOfType(arg);
        }

        /// <summary>Converts this AppBuilder to a constructor middleware factory.</summary>
        /// <param name="middlewareObject">  The middleware object.</param>
        /// <param name="args">              The arguments.</param>
        /// <param name="middlewareDelegate">The middleware delegate.</param>
        /// <returns>The given data converted to a Tuple{Type,Delegate,object[]}</returns>
        private static Tuple<Type, Delegate, object[]> ToConstructorMiddlewareFactory(
            object middlewareObject,
            object[] args,
            ref Delegate middlewareDelegate)
        {
            var type = middlewareObject as Type;
            var constructors = type.GetConstructors();
            for (var i = 0; i < constructors.Length; i++)
            {
                var constructorInfo = constructors[i];
                var parameters = constructorInfo.GetParameters();
                var array = (from p in (IEnumerable<ParameterInfo>)parameters select p.ParameterType).ToArray();
                if (array.Length == args.Length + 1)
                {
                    if (array.Skip(1).Zip(args, TestArgForParameter).All(x => x))
                    {
                        var parameterExpressionArray = (
                            from p in (IEnumerable<ParameterInfo>)parameters
                            select Expression.Parameter(p.ParameterType, p.Name)).ToArray();
                        var newExpression = Expression.New(constructorInfo, parameterExpressionArray);
                        middlewareDelegate = Expression.Lambda(newExpression, parameterExpressionArray).Compile();
                        return Tuple.Create(parameters[0].ParameterType, middlewareDelegate, args);
                    }
                }
            }
            throw new MissingMethodException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.Exception_NoConstructorFound,
                    type.FullName,
                    args.Length + 1));
        }

        /// <summary>Converts this AppBuilder to a generator middleware factory.</summary>
        /// <param name="middlewareObject">The middleware object.</param>
        /// <param name="args">            The arguments.</param>
        /// <returns>The given data converted to a Tuple{Type,Delegate,object[]}</returns>
        private static Tuple<Type, Delegate, object[]> ToGeneratorMiddlewareFactory(
            object middlewareObject,
            object[] args)
        {
            var methods = middlewareObject.GetType().GetMethods();
            for (var i = 0; i < methods.Length; i++)
            {
                var methodInfo = methods[i];
                if (methodInfo.Name == "Invoke")
                {
                    var parameters = methodInfo.GetParameters();
                    var array = (from p in (IEnumerable<ParameterInfo>)parameters select p.ParameterType).ToArray();
                    if (array.Length == args.Length + 1)
                    {
                        if (array.Skip(1).Zip(args, TestArgForParameter).All(x => x))
                        {
                            var @delegate = Delegate.CreateDelegate(
                                Expression.GetFuncType(array.Concat(new[] { methodInfo.ReturnType }).ToArray()),
                                middlewareObject,
                                methodInfo);
                            return Tuple.Create(parameters[0].ParameterType, @delegate, args);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>Converts this AppBuilder to an instance middleware factory.</summary>
        /// <param name="middlewareObject">The middleware object.</param>
        /// <param name="args">            The arguments.</param>
        /// <returns>The given data converted to a Tuple{Type,Delegate,object[]}</returns>
        private static Tuple<Type, Delegate, object[]> ToInstanceMiddlewareFactory(
            object middlewareObject,
            object[] args)
        {
            var methods = middlewareObject.GetType().GetMethods();
            for (var i = 0; i < methods.Length; i++)
            {
                var methodInfo = methods[i];
                if (methodInfo.Name == "Initialize")
                {
                    var parameters = methodInfo.GetParameters();
                    var typeArray = (from p in (IEnumerable<ParameterInfo>)parameters select p.ParameterType).ToArray();
                    if (typeArray.Length == args.Length + 1)
                    {
                        if (typeArray.Skip(1).Zip(args, TestArgForParameter).All(x => x))
                        {
                            Func<object, object> func = app =>
                            {
                                var array = new[] { app }.Concat(args).ToArray();
                                methodInfo.Invoke(middlewareObject, array);
                                return middlewareObject;
                            };
                            return Tuple.Create<Type, Delegate, object[]>(
                                parameters[0].ParameterType,
                                func,
                                Array.Empty<object>());
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>Converts this AppBuilder to a member delegate.</summary>
        /// <param name="signature">The signature.</param>
        /// <param name="app">      The application.</param>
        private static Delegate ToMemberDelegate(Type signature, object app)
        {
            var method = signature.GetMethod("Invoke");
            var parameters = method.GetParameters();
            var methods = app.GetType().GetMethods();
            for (var i = 0; i < methods.Length; i++)
            {
                var methodInfo = methods[i];
                if (methodInfo.Name == "Invoke")
                {
                    var parameterInfoArray = methodInfo.GetParameters();
                    if (parameterInfoArray.Length == parameters.Length)
                    {
                        if (!parameterInfoArray.Zip(
                                    parameters,
                                    (methodParameter, signatureParameter)
                                        => methodParameter.ParameterType.IsAssignableFrom(
                                            signatureParameter.ParameterType))
                                .Any(compatible => !compatible)
                            && method.ReturnType.IsAssignableFrom(methodInfo.ReturnType))
                        {
                            return Delegate.CreateDelegate(signature, app, methodInfo);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>Converts this AppBuilder to a middleware factory.</summary>
        /// <param name="middlewareObject">The middleware object.</param>
        /// <param name="args">            The arguments.</param>
        /// <returns>The given data converted to a Tuple{Type,Delegate,object[]}</returns>
        private static Tuple<Type, Delegate, object[]> ToMiddlewareFactory(object middlewareObject, object[] args)
        {
            if (middlewareObject == null)
            {
                throw new ArgumentNullException(nameof(middlewareObject));
            }
            var @delegate = middlewareObject as Delegate;
            if (@delegate != null)
            {
                return Tuple.Create(GetParameterType(@delegate), @delegate, args);
            }
            var instanceMiddlewareFactory = ToInstanceMiddlewareFactory(middlewareObject, args);
            if (instanceMiddlewareFactory != null)
            {
                return instanceMiddlewareFactory;
            }
            instanceMiddlewareFactory = ToGeneratorMiddlewareFactory(middlewareObject, args);
            if (instanceMiddlewareFactory != null)
            {
                return instanceMiddlewareFactory;
            }
            if (!(middlewareObject is Type))
            {
                throw new NotSupportedException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Exception_MiddlewareNotSupported,
                        middlewareObject.GetType().FullName));
            }
            return ToConstructorMiddlewareFactory(middlewareObject, args, ref @delegate);
        }

        /// <summary>Adds a signature conversion.</summary>
        /// <param name="conversion">The conversion.</param>
        private void AddSignatureConversion(Delegate conversion)
        {
            if (conversion == null)
            {
                throw new ArgumentNullException(nameof(conversion));
            }
            var parameterType = GetParameterType(conversion);
            if (parameterType == null)
            {
                throw new ArgumentException(Resources.Exception_ConversionTakesOneParameter, nameof(conversion));
            }
            var tuple = Tuple.Create(conversion.Method.ReturnType, parameterType);
            _conversions[tuple] = conversion;
        }

        /// <summary>Builds an internal.</summary>
        /// <param name="signature">The signature.</param>
        /// <returns>An object.</returns>
        private object BuildInternal(Type signature)
        {
            if (!Properties.TryGetValue("builder.DefaultApp", out var notFound))
            {
                notFound = NotFound;
            }
            foreach (var tuple in _middleware.Reverse())
            {
                var item1 = tuple.Item1;
                var item2 = tuple.Item2;
                var item3 = tuple.Item3;
                notFound = Convert(item1, notFound);
                var array = new[] { notFound }.Concat(item3).ToArray();
                notFound = item2.DynamicInvoke(array);
                notFound = Convert(item1, notFound);
            }
            return Convert(signature, notFound);
        }

        /// <summary>Converts.</summary>
        /// <param name="signature">The signature.</param>
        /// <param name="app">      The application.</param>
        /// <returns>An object.</returns>
        private object Convert(Type signature, object app)
        {
            if (app == null)
            {
                return null;
            }
            var obj = ConvertOneHop(signature, app);
            if (obj != null)
            {
                return obj;
            }
            var obj1 = ConvertMultiHop(signature, app);
            if (obj1 == null)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Exception_NoConversionExists,
                        app.GetType(),
                        signature),
                    nameof(signature));
            }
            return obj1;
        }

        /// <summary>Convert multi hop.</summary>
        /// <param name="signature">The signature.</param>
        /// <param name="app">      The application.</param>
        /// <returns>The multi converted hop.</returns>
        private object ConvertMultiHop(Type signature, object app)
        {
            object obj;
            using var enumerator = _conversions.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                var obj1 = ConvertOneHop(current.Key.Item2, app);
                if (obj1 == null)
                {
                    continue;
                }
                var obj2 = current.Value.DynamicInvoke(obj1);
                if (obj2 == null)
                {
                    continue;
                }
                var obj3 = ConvertOneHop(signature, obj2);
                if (obj3 == null)
                {
                    continue;
                }
                obj = obj3;
                return obj;
            }
            return null;
        }

        /// <summary>Convert one hop.</summary>
        /// <param name="signature">The signature.</param>
        /// <param name="app">      The application.</param>
        /// <returns>The one converted hop.</returns>
        private object ConvertOneHop(Type signature, object app)
        {
            object obj;
            if (signature.IsInstanceOfType(app))
            {
                return app;
            }
            if (typeof(Delegate).IsAssignableFrom(signature))
            {
                var memberDelegate = ToMemberDelegate(signature, app);
                if (memberDelegate != null)
                {
                    return memberDelegate;
                }
            }
            using var enumerator = _conversions.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                var item1 = current.Key.Item1;
                if (!current.Key.Item2.IsInstanceOfType(app) || !signature.IsAssignableFrom(item1))
                {
                    continue;
                }
                obj = current.Value.DynamicInvoke(app);
                return obj;
            }
            return null;
        }
    }
}
