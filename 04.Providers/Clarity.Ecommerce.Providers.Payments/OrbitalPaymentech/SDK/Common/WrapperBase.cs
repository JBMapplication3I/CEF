using System;
using System.Reflection;
using System.Runtime.CompilerServices;
namespace JPMC.MSDK.Common
{
/// <summary>
/// Title: Wrapper Base class
///
/// Description: This abstract class can be used as a base class for
///	Wrapper classes that need to have the ability to be replaced
///	with a Wrapper Stub during testing
///
/// Copyright (c) 2018, Chase Paymentech Solutions, LLC. All rights
/// reserved
///
/// @author Frank McCanna
/// @version 1.0
/// </summary>
[System.Runtime.InteropServices.ComVisible(false)]
public abstract class WrapperBase<T> where T : new()
{
	private static WrapperFactory<T> factory = new WrapperFactory<T>();

	/// <summary>
	/// This method allows the wrapper to be replaced with a stub during
	/// testing.
	/// </summary>
	/// <param name="stubtype"></param>
	public static void SetWrapperStub( Type stubtype )
	{
		factory.DerivedClass = stubtype;
	}

	/// <summary>
	/// Returns an object of the type configured
	/// </summary>
	/// <returns>type configured.</returns>
	public static T GetInstance()
	{
		return factory.GetInstance();
	}
}
}
