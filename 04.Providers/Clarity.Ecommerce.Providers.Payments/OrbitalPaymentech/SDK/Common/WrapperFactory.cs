using System;
using System.Collections.Generic;
using System.Reflection;

namespace JPMC.MSDK.Common
{
/// <summary>
/// Title: Wrapper Factory
///
/// Description: This generic class can be used as a factory for all
///	Wrapper classes that need to have the ability to be replaced
///	with a Wrapper Stub during testing
///
/// Copyright (c) 2018, Chase Paymentech Solutions, LLC. All rights
/// reserved
///
/// @author Frank McCanna
/// @version 1.0
///
/// </summary>
/// <typeparam name="T"></typeparam>
public class WrapperFactory<T> where T : new()
{
	/// <summary>
	/// This class is created with the base Wrapper class, which
	/// we save for returning a production object or for checking
	/// to be sure the Stub class is derived from the base Wrapper class
	/// </summary>
	private Type baseClass = typeof( T );

	/// <summary>
	/// The following is the Stub class that will be set in a test 
	/// environment.  In a production environment this variable
	/// will remain null, and the baseClass will be used to 
	/// create the Wrapper object.
	/// </summary>
	private Type derivedClass;

	/// <summary>
	/// Get an object of the Wrapper class (production), or, if derivedClass is set (test) 
	/// return an object of that type (but down cast to the base Wrapper class)
	/// </summary>
	/// <returns>Always returns the base class because that is what the code expects.</returns>
	public T GetInstance()
	{
		if ( derivedClass == null )
		{
			return new T();
		}
		else
		{
			// This static call off a Type class will generate an object
			// of that type
			return ( T ) derivedClass.InvokeMember( null, 
				BindingFlags.DeclaredOnly | 
				BindingFlags.Public | BindingFlags.NonPublic | 
				BindingFlags.Instance | 
				BindingFlags.CreateInstance, null, null, null );
		}
	}

	/// <summary>
	/// Property for the stub class variable
	/// </summary>
	public Type DerivedClass
	{
		get => derivedClass;
        set
		{
			// We can only allow setting the Stub class to a class which is
			// derived from the original Wrapper class
			if ( value.BaseType != baseClass )
			{
				throw new Exception( "This WrapperFactory requires a class derived from: " 
					+ baseClass.Name );
			}
			else
			{
				derivedClass = value;
			}
		}
	}
}

}
