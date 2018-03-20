using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace Redbridge.Gaming
{
	public class Spinner<T>
	{
		private List<T> _spinValues = new List<T>();
		private BehaviorSubject<T> _currentValue = new BehaviorSubject<T>(default(T));
		private Random _randomizer;

		public Spinner()
		{
			_randomizer = new Random(GetHashCode());
		}

		public Spinner(params T[] values) : this()
		{
			_spinValues.AddRange(values);
		}

		public IObservable<T> Value
		{
			get { return _currentValue; }
		}

		public T Spin()
		{
			var index = _randomizer.Next(0, _spinValues.Count - 1);
			var value = _spinValues[index];
			_currentValue.OnNext(value);
			return value;
		}
	}
}
