using System;
using System.Linq.Expressions;
using NUnit.Framework;
using Redbridge.Linq.Expressions;

namespace Redbridge.Tests
{
    [TestFixture]
    public class ExpressionExtensionsTest
    {
        public class ClassWithPropertyName
        {
            public ClassWithPropertyName() { }

            public int MyPropertyName
            {
                get;
                set;
            }
        }
        
        [Test]
        public void GetPropertyNameExtensionMethod_ExpectSuccessfulReturnOfTheLambdaProperty()
        {
            ClassWithPropertyName instance = new ClassWithPropertyName();
            Expression<Func<ClassWithPropertyName, int>> expression = (i) => instance.MyPropertyName;
            string name = expression.GetPropertyName();
            Assert.AreEqual("MyPropertyName", name);
        }
    }
}
