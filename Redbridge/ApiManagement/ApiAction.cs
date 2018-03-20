using System;
using System.Threading.Tasks;
using Redbridge.Diagnostics;
using Redbridge.SDK;
using Redbridge.Validation;

namespace Redbridge.ApiManagement
{
public abstract class ApiAction<TIn1, TContext> : ApiCall
{
	private readonly IObjectValidator _validator;
	private readonly IApiContextProvider<TContext> _contextProvider;
	private readonly IApiContextAuthorizer<TContext> _authority;

	protected ApiAction(IObjectValidator validator, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority) : base(logger)
	{
		if (authority == null)throw new ArgumentNullException(nameof(authority));
		if (validator == null) throw new ArgumentNullException(nameof(validator));
		if (contextProvider == null) throw new ArgumentNullException(nameof(contextProvider));

		_authority = authority;
		_validator = validator;
		_contextProvider = contextProvider;
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

	protected virtual void OnBeforeInvoke(TIn1 in1)
	{
		_validator.Validate(in1).OnFailThrowValidationException();
	}

	protected virtual void OnAfterInvoke() { }
}

	public abstract class ApiAction<TContext> : ApiCall
	{
		private readonly IApiContextProvider<TContext>  _contextProvider;
		private readonly IApiContextAuthorizer<TContext> _authority;

			protected ApiAction(ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority) : base(logger)
		{	
			if (authority == null) throw new ArgumentNullException(nameof(authority));
			if (contextProvider == null) throw new ArgumentNullException(nameof(contextProvider));
			_authority = authority;
			_contextProvider = contextProvider;
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
