using NUnit.Framework;
using System;
using Redbridge.Data.Mapping;

namespace Redbridge.SDK.Tests
{
    [TestFixture()]
    public class FieldMappingTests
    {
        public class SomeRecord
        {
            public string Name { get; set; }
        }

        [Test()]
        public void CreateFieldMapperReadStringFromRecord()
        {
            var mapper = new FieldMap<SomeRecord, string>("Name", (r) => r.Name);
            var result = mapper.Read(new SomeRecord() { Name = "Tester" });
            Assert.AreEqual("Tester", result);
        }

		[Test()]
		public void CreateFieldMapperReadStringWithConversionToBoolFromRecord()
		{
            var mapper = new FieldMap<SomeRecord, string, bool>("Name", (r) => r.Name, (o) => o != null ? ((o).ToString() == "Yes" ? true : false) : false);
			var result1 = mapper.Read(new SomeRecord() { Name = "Yes" });
			Assert.IsTrue(result1);

			var result2 = mapper.Read(new SomeRecord() { Name = "Else" });
			Assert.IsFalse(result2);
		}
    }
}
