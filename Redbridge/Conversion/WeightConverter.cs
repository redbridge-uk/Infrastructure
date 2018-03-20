using System;
namespace Redbridge.Converters
{
	public class ImperialWeight
	{
		public const decimal OuncesInAKilogram = PoundsInAKilogram * OuncesPerPound;
		public const decimal PoundsInAKilogram = 2.20462M;
		public const decimal OuncesPerPound = 16M;
		public const decimal OuncesPerStone = 224M;
		public const decimal PoundsPerStone = 14M;

		public ImperialWeight() { }

		public ImperialWeight(decimal totalOunces)
		{
			TotalOunces = totalOunces;
		}

		public static ImperialWeight FromStonePoundsAndOunces(int stone, int pound, decimal ounces)
		{
			return new ImperialWeight((stone * OuncesPerStone) + (pound * OuncesPerPound) + ounces);
		}

		public static ImperialWeight FromKilos(decimal kgs)
		{
			return new ImperialWeight() { TotalOunces = kgs * OuncesInAKilogram };
		}

		public static ImperialWeight FromOunces(decimal ounces)
		{
			return new ImperialWeight() { TotalOunces = ounces };
		}

		public static ImperialWeight FromPounds(decimal pounds)
		{
			return new ImperialWeight() { TotalOunces = pounds * OuncesPerPound };
		}

		public int Pounds 
		{
			get 
			{
				if (TotalPounds <= decimal.Zero) return 0;
				// FIgure out the pounds component is the total number of pounds remaining after the stone component
				// is calculated.
				var stoneFactor = Stone * PoundsPerStone;
				var remainingPounds = TotalPounds - stoneFactor;
				var result = remainingPounds;
				return Convert.ToInt32(Math.Truncate(result));           
			}
		}

		// The ounces part (not total ounces)
		public decimal Ounces 
		{
			get 
			{
				if (TotalOunces <= 0) return decimal.Zero;
				var stoneOunces = Stone * OuncesPerStone;
				var poundOunces = Pounds * OuncesPerPound;
				return TotalOunces - stoneOunces - poundOunces;
			}
		}

		public int Stone 
		{ 
			get
			{
				var result = (TotalPounds / PoundsPerStone);
				return Convert.ToInt32(Math.Truncate(result));
			}
		}

		public decimal TotalOunces { get; set; }

		public decimal TotalKilos
		{
			get { return Math.Round(TotalOunces / OuncesInAKilogram, 3); }
		}

		public decimal TotalPounds
		{
			get { return Math.Round(TotalOunces / OuncesPerPound, 3); }
		}

		public decimal TotalStone
		{
			get { return TotalOunces / OuncesPerPound / PoundsPerStone; }
		}
	}

	public static class WeightConverter
	{
		public static decimal ToMetric(ImperialWeight weight)
		{
			if (weight == null) throw new ArgumentNullException(nameof(weight));
			return weight.TotalKilos;
		}



		public static ImperialWeight FromKilos (decimal kilograms)
		{
			return new ImperialWeight()
			{
				TotalOunces = kilograms * ImperialWeight.PoundsInAKilogram * ImperialWeight.OuncesPerPound
			};
		}

		public static decimal ToKilos(ImperialWeight result)
		{
			return result.TotalKilos;
		}

		public static decimal ToKilosFromOunces(decimal ounces)
		{
			var imperial = ImperialWeight.FromOunces(ounces);
			return imperial.TotalKilos;
		}

		public static decimal ToKilosFromPounds(decimal pounds)
		{
			var imperial = ImperialWeight.FromPounds(pounds);
			return imperial.TotalKilos;
		}
	}
}
