using System;
using System.Threading.Tasks;
using Redbridge.Diagnostics;

namespace Redbridge.ApiManagement
{
	public abstract class UnitOfWorkActionApi<TUnitOfWork, TContext> : ApiAction<TContext>
		where TUnitOfWork : IWorkUnit
	{
		private readonly IUnitOfWorkFactory<TUnitOfWork> _unitOfWorkFactory;

		protected UnitOfWorkActionApi(IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
			: base(logger, contextProvider, authority)
		{
            _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
		}

        protected IUnitOfWorkFactory<TUnitOfWork> Factory => _unitOfWorkFactory;

		protected override async Task OnInvoke(TContext context)
		{
			using (var unit = _unitOfWorkFactory.Create())
			{
				await OnInvoke(unit, context);
				try
				{
					await unit.SaveChangesAsync();
					await OnCommitCompleted(context);
				}
				catch (Exception e)
				{
					await OnCommitFailed(e, context);
					throw;
				}
			}
		}

		protected virtual Task OnCommitCompleted(TContext context)
		{
			return Task.FromResult(true);
		}

		protected virtual Task OnCommitFailed(Exception exception, TContext context)
		{
			return Task.FromResult(true);
		}

		protected abstract Task OnInvoke(TUnitOfWork unit, TContext context);
	}

	public abstract class UnitOfWorkActionApi<TIn1, TUnitOfWork, TContext> : ApiAction<TIn1, TContext>
	where TUnitOfWork : IWorkUnit
	{
		private readonly IUnitOfWorkFactory<TUnitOfWork> _unitOfWorkFactory;

		protected UnitOfWorkActionApi(IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
			: base(logger, contextProvider, authority)
		{
			_unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
		}

        protected IUnitOfWorkFactory<TUnitOfWork> Factory => _unitOfWorkFactory;

		protected override async Task OnInvoke(TIn1 in1, TContext context)
		{
			using (var unit = _unitOfWorkFactory.Create())
			{
				await OnInvoke(unit, in1, context);
				try
				{
					await unit.SaveChangesAsync();
					await OnCommitCompleted(unit, in1, context);
				}
				catch (Exception e)
				{
					await OnCommitFailed(unit, e, in1, context);
					throw;
				}
			}
		}

		protected virtual Task OnCommitCompleted(TUnitOfWork workUnit, TIn1 in1, TContext context)
		{
			return Task.FromResult(true);
		}

		protected virtual Task OnCommitFailed(TUnitOfWork workUnit, Exception exception, TIn1 in1, TContext context)
		{
			return Task.FromResult(true);
		}

		protected abstract Task OnInvoke(TUnitOfWork unit, TIn1 in1, TContext context);
	}
}
