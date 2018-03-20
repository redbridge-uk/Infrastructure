//using System.Collections.Generic;
//using System.Data.Entity.Infrastructure;
//using System.Data.Entity.Validation;
//using System;
//using System.Linq;
//using Redbridge.SDK;

//namespace Redbridge.EntityFramework
//{
//	public static class DbEntityValidationResultExtensions
//	{
//		public static ValidationResultsException ToValidationResultsException(this DbEntityValidationException source)
//		{
//			var validationResults = source.EntityValidationErrors.SelectMany(ev => ev.ToValidationResults()).ToArray();
//			var collection = new ValidationResultCollection(validationResults);
//			var message = string.Join(",", validationResults.Select(r => r.Message));
//			return new ValidationResultsException(message, collection);
//		}

//		public static ValidationResultsException ToValidationResultsException(this DbUpdateException source)
//		{
//			var result = ValidationResult.Fail(string.Empty, source.Message);
//			return source.InnerException == null ? new ValidationResultsException(new[] { result }) : new ValidationResultsException(new[] { ValidationResult.Fail(string.Empty, source.InnerException.Message) });
//		}

//		public static IEnumerable<ValidationResult> ToValidationResults(this DbEntityValidationResult result)
//		{
//			return result.ValidationErrors.Select(ve => new ValidationResult(false, ve.ErrorMessage) { PropertyName = ve.PropertyName });
//		}
//	}
//}
