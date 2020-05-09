using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Redbridge.Exceptions;
using Redbridge.Linq;

namespace Redbridge.Validation
{
	public class ValidationResultsException : ValidationException
	{
		public ValidationResultsException() { }

		public ValidationResultsException(IEnumerable<ValidationResult> results)
			: base(results != null ? results.First(r => !r.Success).Message : string.Empty)
		{
		}

		public ValidationResultsException(ValidationResultCollection results) : base(results != null ? results.Message : string.Empty)
		{
			if (results == null) throw new ArgumentNullException(nameof(results));
			Results = results;
		}

		public ValidationResultsException(string message, ValidationResultCollection results) : base(message)
		{
			if (results == null) throw new ArgumentNullException(nameof(results));
			Results = results;
		}

		public ValidationResultCollection Results
		{
			get;
			private set;
		}

		public override string ToString()
		{
			var builder = new StringBuilder("The following validation errors have occurred:");
			Results?.Results.ForEach(r => builder.AppendLine(r.ToString()));
			return builder.ToString();
		}
	}
}
