using System;
using System.Collections.Generic;

namespace Redbridge.Validation
{
	public class ValidationResultCollection : ValidationResult
	{
		private readonly List<ValidationResult> _results = new List<ValidationResult>();
		private bool _isDefaultMessage = true;

		public ValidationResultCollection() : this(true) { }

		public ValidationResultCollection(bool success) : base(success)
		{
			if (!success)
			{
				Message = "Validation failed. Please see inner results for details.";
			}
		}

		public ValidationResultCollection(IEnumerable<ValidationResult> results, string message = "") : this(true)
		{
			if (results == null) throw new ArgumentNullException(nameof(results));
			AddRange(results);

			if (!string.IsNullOrWhiteSpace(message))
				Message = message;
		}

		public static ValidationResultCollection Empty => new ValidationResultCollection(true);

		public void AddRange(IEnumerable<ValidationResult> results)
		{
			foreach (var result in results)
			{
				AddResult(result);
			}
		}

        public void Add (ValidationResult result)
        {
            AddResult(result);
        }

        public void Add (bool result, string message)
        {

            Add(new ValidationResult(result, message));
        }

		public void AddResult(ValidationResult result)
		{
			if (result == null) throw new ArgumentNullException(nameof(result), "The result is null.");
			_results.Add(result);

			// If any one of the results is a failure, then the overriding result of this collection should be a failure.
			if (!result.Success)
			{
				Success = false;
			}

			// This will set the validation message to the first message in the list.
			if (_isDefaultMessage)
			{
				Message = result.Message;
				_isDefaultMessage = false;
			}
		}

		public IEnumerable<ValidationResult> Results => _results;
	}
}
