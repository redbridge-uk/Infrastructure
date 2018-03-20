using System;
using System.Collections.Generic;

namespace Redbridge.Validation
{
	public abstract class ValueRestrictionValidator<T> : Validator
	{
		private List<T> _restrictions = new List<T>();

		protected ValueRestrictionValidator() { }

		protected ValueRestrictionValidator(T[] restrictions)
		{
			PermittedValues = restrictions;
		}

		public T[] PermittedValues
		{
			get { return _restrictions.ToArray(); }
			set
			{
				_restrictions = new List<T>(value);
			}
		}

		public void AddRestriction(T restriction)
		{
			_restrictions.Add(restriction);
		}
	}
}
