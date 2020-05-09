using NUnit.Framework;
using Redbridge.Conversion;

namespace Redbridge.Tests
{
	[TestFixture()]
	public class WeightConverterTests
	{
		[Test()]
		public void TestConvertMetricToImperialZeroValue()
		{
			var result = WeightConverter.FromKilos(decimal.Zero);
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.Pounds);
			Assert.AreEqual(0, result.Ounces);
			Assert.AreEqual(0, result.TotalOunces);
		}

		[Test()]
		public void TestConvertMetricToImperial80KgsTotalPoundsDecimalValue()
		{
			var result = WeightConverter.FromKilos(80M);
			Assert.IsNotNull(result);
			Assert.AreEqual(176.37M, result.TotalPounds);
			Assert.AreEqual(12, result.Stone);
			Assert.AreEqual(8, result.Pounds);
			Assert.AreEqual(5.9136M, result.Ounces);
		}

		[Test()]
		public void TestConvertImperialToMetric80KgsTotalPoundsAndBackDecimalValue()
		{
			var result = WeightConverter.FromKilos(80M);
			Assert.IsNotNull(result);
			Assert.AreEqual(176.37M, result.TotalPounds);

			var kilos = WeightConverter.ToKilos(result);
			Assert.AreEqual(80M, kilos);
		}

		[Test]
		public void TestConvertImperialToMetric()
		{
			var result = ImperialWeight.FromStonePoundsAndOunces(12, 8, 5.9136M);
			Assert.AreEqual(176.37M, result.TotalPounds);
			Assert.AreEqual(12, result.Stone);
			Assert.AreEqual(8, result.Pounds);
			Assert.AreEqual(5.9136M, result.Ounces);
			Assert.AreEqual(80M, result.TotalKilos);
		}
	}
}
