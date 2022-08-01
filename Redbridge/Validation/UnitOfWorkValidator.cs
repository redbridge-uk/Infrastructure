using System;
using System.Threading.Tasks;
using Redbridge.DependencyInjection;

namespace Redbridge.Validation
{
	public abstract class UnitOfWorkValidator<TInput, TWorkUnit>
	where TWorkUnit: class, IWorkUnit
	{
		protected UnitOfWorkValidator(TWorkUnit workUnit)
		{
			if (workUnit == null) throw new ArgumentNullException(nameof(workUnit));
			WorkUnit = workUnit;
		}

		protected TWorkUnit WorkUnit { get; }

		public Task ValidateAsync(TInput input)
		{
			return OnValidateAsync(input);
		}

		protected abstract Task OnValidateAsync(TInput input);
	}
}
