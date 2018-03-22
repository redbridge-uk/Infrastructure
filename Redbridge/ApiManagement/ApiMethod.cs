using System;
using System.Threading.Tasks;
using Redbridge.Diagnostics;
using Redbridge.SDK;
using Redbridge.Validation;

namespace Redbridge.ApiManagement
{
	public abstract class ApiMethod<TIn1, TIn2, TIn3, TReturn, TContext> : ApiCall
	{
		private readonly IApiContextProvider<TContext> _contextProvider;
		readonly IApiContextAuthorizer<TContext> _authority;

		protected ApiMethod(ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
				: base(logger)
		{
			
			if (authority == null) throw new ArgumentNullException(nameof(authority));
			if (contextProvider == null) throw new ArgumentNullException(nameof(contextProvider));
			_contextProvider = contextProvider;
			_authority = authority;
		}

		public async Task<TReturn> InvokeAsync(TIn1 in1, TIn2 in2, TIn3 in3)
		{
			OnBeforeInvoke(in1, in2, in3);

			var context = await _contextProvider.GetCurrentAsync();

			await _authority.AuthorizeAsync(this, context);

			var result = await OnInvoke(in1, in2, in3, context);

			OnAfterInvoke(in1, in2, in3, result);

			return result;
		}

		protected abstract Task<TReturn> OnInvoke(TIn1 in1, TIn2 in2, TIn3 in3, TContext context);

		protected virtual void OnBeforeInvoke(TIn1 in1, TIn2 in2, TIn3 in3) {}

		protected virtual void OnAfterInvoke(TIn1 in1, TIn2 in2, TIn3 in3, TReturn result) {}
	}


	public abstract class ApiMethod<TIn1, TIn2, TReturn, TContext> : ApiCall
	{
		private readonly IApiContextProvider<TContext> _contextProvider;
		private readonly IApiContextAuthorizer<TContext> _authority;

		protected ApiMethod(ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority) : base(logger)
		{
			
			if (authority == null) throw new ArgumentNullException(nameof(authority));
			if (contextProvider == null) throw new ArgumentNullException(nameof(contextProvider));
			_contextProvider = contextProvider;
            _authority = authority;
		}

		public async Task<TReturn> InvokeAsync(TIn1 in1, TIn2 in2)
		{
			OnBeforeInvoke(in1, in2);

			var context = await _contextProvider.GetCurrentAsync();

			await _authority.AuthorizeAsync(this, context);

			var result = await OnInvoke(in1, in2, context);

			OnAfterInvoke(in1, in2, result);

			return result;
		}

		protected abstract Task<TReturn> OnInvoke(TIn1 in1, TIn2 in2, TContext context);

		protected virtual void OnBeforeInvoke(TIn1 in1, TIn2 in2)
		{
		}

		protected virtual void OnAfterInvoke(TIn1 in1, TIn2 in2, TReturn result) { }
	}

	public abstract class ApiMethod<TIn1, TReturn, TContext> : ApiCall
	{
		private readonly IApiContextProvider<TContext> _contextProvider;
		readonly IApiContextAuthorizer<TContext> _authority;

		protected ApiMethod(ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
		: base(logger)
		{

			if (authority == null) throw new ArgumentNullException(nameof(authority));
			if (contextProvider == null) throw new ArgumentNullException(nameof(contextProvider));
			_contextProvider = contextProvider;
			_authority = authority;
		}

		public async Task<TReturn> InvokeAsync(TIn1 in1)
		{

			var context = await _contextProvider.GetCurrentAsync();

			await _authority.AuthorizeAsync(this, context);

			OnBeforeInvoke(in1);

			Logger.WriteDebug($"Invoking API {ApiName} with input {in1}");

			var result = await OnInvoke(in1, context);

			OnAfterInvoke(in1, result);

			return result;
		}

		protected abstract Task<TReturn> OnInvoke(TIn1 in1, TContext context);

		protected virtual void OnBeforeInvoke(TIn1 in1)
		{
		}

		protected virtual void OnAfterInvoke(TIn1 in1, TReturn result) { }
	}

	public abstract class ApiMethod<TReturn, TContext> : ApiCall
	{
		private readonly IApiContextProvider<TContext> _contextProvider;
		readonly IApiContextAuthorizer<TContext> _authority;

		protected ApiMethod(ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority) : base(logger)
		{
			if (authority == null) throw new ArgumentNullException(nameof(authority));
			if (contextProvider == null) throw new ArgumentNullException(nameof(contextProvider));
			_contextProvider = contextProvider;
			_authority = authority;
		}

		public async Task<TReturn> InvokeAsync()
		{
			OnBeforeInvoke();

			var context = await _contextProvider.GetCurrentAsync();

			await _authority.AuthorizeAsync(this, context);

			var result = await OnInvoke(context);

			OnAfterInvoke(result, context);

			return result;
		}

		protected abstract Task<TReturn> OnInvoke(TContext context);

		protected virtual void OnBeforeInvoke() { }

		protected virtual void OnAfterInvoke(TReturn result, TContext context) { }
	}
}
