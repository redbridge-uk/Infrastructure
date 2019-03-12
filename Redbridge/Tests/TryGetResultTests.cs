﻿using NUnit.Framework;
using Redbridge.SDK;

namespace Redbridge.Tests
{
    [TestFixture()]
    public class TryGetResultTests
    {
        public class TestResult
        {
            
        }

        [Test()]
        public void TryGetResultFromNullableInstance()
        {
            TestResult item = null;
            var result = TryGetResult.FromResult(item);
            Assert.IsFalse(result.Success);
        }
    }
}
