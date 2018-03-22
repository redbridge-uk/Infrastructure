using System;
using System.Threading.Tasks;
using Redbridge.Diagnostics;
using Redbridge.SDK;
using Redbridge.Validation;

namespace Redbridge.ApiManagement
{
public abstract class ApiAction<TIn1, TContext> : ApiCall
{
	private readonly IApiContextProvider<TContext> _contextProvider;
	private readonly IApiContextAuthorizer<TContext> _authority;

	protected ApiAction(ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority) : base(logger)
	{
		_authority = authority ?? throw new ArgumentNullException(nameof(authority));
		_contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
	}

	public async Task InvokeAsync(TIn1 in1)
	{
		OnBeforeInvoke(in1);

		var context = await _contextProvider.GetCurrentAsync();

		await _authority.AuthorizeAsync(this, context);

		await OnInvoke(in1, context);

		OnAfterInvoke();

	}

	protected abstract Task OnInvoke(TIn1 in1, TContext context);

	protected virtual void OnBeforeInvoke(TIn1 in1)	{	}

	protected virtual void OnAfterInvoke() { }
}

	public abstract class ApiAction<TContext> : ApiCall
	{
		private readonly IApiContextProvider<TContext>  _contextProvider;
		private readonly IApiContextAuthorizer<TContext> _authority;

			protected ApiAction(ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority) : base(logger)
		{
			_authority = authority ?? throw new ArgumentNullException(nameof(authority));
			_contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
		}

		public async Task InvokeAsync()
		{
			OnBeforeInvoke();

			var context = await _contextProvider.GetCurrentAsync();

			await _authority.AuthorizeAsync(this, context);

			await OnInvoke(context);

			OnAfterInvoke();

		}

		protected abstract Task OnInvoke(TContext context);

		protected virtual void OnBeforeInvoke() { }

		protected virtual void OnAfterInvoke() { } }
	}
