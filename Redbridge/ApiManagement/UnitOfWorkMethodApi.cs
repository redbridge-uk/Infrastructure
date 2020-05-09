using System;
using System.Threading.Tasks;
using Redbridge.ApiManagement;
using Redbridge.Diagnostics;
using Redbridge.Validation;

namespace Redbridge.ApiManagement
{
    public abstract class UnitOfWorkMethodApi<TResponse, TUnitOfWork, TContext> : ApiMethod<TResponse, TContext>
    where TUnitOfWork : IWorkUnit
    {
        private readonly IUnitOfWorkFactory<TUnitOfWork> _unitOfWorkFactory;

        protected UnitOfWorkMethodApi(IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
            : base(logger, contextProvider, authority)
        {
            if (unitOfWorkFactory == null) throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        protected IUnitOfWorkFactory<TUnitOfWork> Factory => _unitOfWorkFactory;

        protected override async Task<TResponse> OnInvoke(TContext context)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                var result = await OnInvoke(unit, context);
                try
                {
                    await unit.SaveChangesAsync();
                    await OnCommitCompleted(result, context);
                    return result;
                }
                catch (Exception e)
                {
                    await OnCommitFailed(e, result, context);
                    throw;
                }
            }
        }

        protected virtual Task OnCommitCompleted(TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected virtual Task OnCommitFailed(Exception exception, TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected abstract Task<TResponse> OnInvoke(TUnitOfWork unit, TContext context);
    }

    public abstract class UnitOfWorkMethodApi<TIn1, TResponse, TUnitOfWork, TContext> : ApiMethod<TIn1, TResponse, TContext>
    where TUnitOfWork : IWorkUnit
    {
        private readonly IUnitOfWorkFactory<TUnitOfWork> _unitOfWorkFactory;

        protected UnitOfWorkMethodApi(IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
            : base(logger, contextProvider, authority)
        {
            _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        protected IUnitOfWorkFactory<TUnitOfWork> Factory => _unitOfWorkFactory;

        protected override async Task<TResponse> OnInvoke(TIn1 in1, TContext context)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                var result = await OnInvoke(unit, in1, context);

                try
                {
                    await unit.SaveChangesAsync();
                    await OnCommitCompleted(in1, result, context);
                }
                catch (Exception e)
                {
                    await OnCommitFailed(e, in1, result, context);
                    throw;
                }

                return result;
            }
        }

        protected virtual Task OnCommitCompleted(TIn1 in1, TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected virtual Task OnCommitFailed(Exception exception, TIn1 in1, TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected abstract Task<TResponse> OnInvoke(TUnitOfWork unit, TIn1 in1, TContext context);
    }

    public abstract class UnitOfWorkMethodApi<TIn1, TIn2, TResponse, TUnitOfWork, TContext> : ApiMethod<TIn1, TIn2, TResponse, TContext>
    where TUnitOfWork : IWorkUnit
    {
        private readonly IUnitOfWorkFactory<TUnitOfWork> _unitOfWorkFactory;

        protected UnitOfWorkMethodApi(IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
            : base(logger, contextProvider, authority)
        {
            _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        protected IUnitOfWorkFactory<TUnitOfWork> Factory => _unitOfWorkFactory;

        protected override async Task<TResponse> OnInvoke(TIn1 in1, TIn2 in2, TContext context)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                var result = await OnInvoke(unit, in1, in2, context);
                try
                {
                    await unit.SaveChangesAsync();
                    await OnCommitCompleted(in1, in2, result, context);
                    return result;
                }
                catch (Exception e)
                {
                    await OnCommitFailed(e, in1, in2, result, context);
                    throw;
                }
            }
        }

        protected virtual Task OnCommitCompleted(TIn1 in1, TIn2 in2, TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected virtual Task OnCommitFailed(Exception exception, TIn1 in1, TIn2 in2, TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected abstract Task<TResponse> OnInvoke(TUnitOfWork unit, TIn1 in1, TIn2 in2, TContext context);
    }

    public abstract class UnitOfWorkMethodApi<TIn1, TIn2, TIn3, TResponse, TUnitOfWork, TContext> : ApiMethod<TIn1, TIn2, TIn3, TResponse, TContext>
    where TUnitOfWork : IWorkUnit
    {
        private readonly IUnitOfWorkFactory<TUnitOfWork> _unitOfWorkFactory;

        protected UnitOfWorkMethodApi(IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
            : base(logger, contextProvider, authority)
        {
            _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        protected IUnitOfWorkFactory<TUnitOfWork> Factory => _unitOfWorkFactory;

        protected override async Task<TResponse> OnInvoke(TIn1 in1, TIn2 in2, TIn3 in3, TContext context)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                var result = await OnInvoke(unit, in1, in2, in3, context);
                await unit.SaveChangesAsync();
                return result;
            }
        }

        protected abstract Task<TResponse> OnInvoke(TUnitOfWork unit, TIn1 in1, TIn2 in2, TIn3 in3, TContext context);
    }
}
