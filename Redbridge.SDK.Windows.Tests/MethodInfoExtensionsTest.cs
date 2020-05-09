using System.ServiceModel;
using NUnit.Framework;
using Redbridge.Reflection;

namespace Redbridge.Windows.Tests
{
[TestFixture]
public class MethodInfoExtensionsTest
{
	public interface ITestInterface
	{
		[OperationContract]
		string DoSomething();

		string DoSomethingWithoutAttribute();
	}

	[Test]
	public void MethodInfoExtension_CheckAttributeIsPresent()
	{
		Assert.IsTrue(typeof(ITestInterface).GetMethod("DoSomething").HasAttribute<OperationContractAttribute>());
	}

	[Test]
	public void MethodInfoExtension_GetAttributeIsPresent()
	{
		Assert.IsNotNull(typeof(ITestInterface).GetMethod("DoSomething").GetAttribute<OperationContractAttribute>());
	}

	[Test]
	public void MethodInfoExtension_CheckAttributeIsNotPresent()
	{
		Assert.IsFalse(typeof(ITestInterface).GetMethod("DoSomethingWithoutAttribute").HasAttribute<OperationContractAttribute>());
	}

	[Test]
	public void MethodInfoExtension_GetAttributeIsNotPresentReturnsNull()
	{
		Assert.IsNull(typeof(ITestInterface).GetMethod("DoSomethingWithoutAttribute").GetAttribute<OperationContractAttribute>());
	} 
	}
}
