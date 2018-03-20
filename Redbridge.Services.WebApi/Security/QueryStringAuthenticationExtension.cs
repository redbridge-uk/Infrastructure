using System;
using Owin;
using Redbridge.SDK;

namespace Redbridge.Services.WebApi.Security
{
	public static class QueryStringAuthenticationExtension
	{
		public static void UseQueryStringAuthentication(this IAppBuilder app)
		{
			app.Use(async (context, next) =>
			{
				if (context.Request.QueryString.HasValue)
				{
					if (string.IsNullOrWhiteSpace(context.Request.Headers.Get(HeaderNames.Authorization)))
					{
						var queryString = HttpUtility.ParseQueryString(context.Request.QueryString.Value);
						if (queryString.ContainsKey(QueryStringParts.Authentication))
						{
							var token = queryString[QueryStringParts.Authentication];

							if (!string.IsNullOrWhiteSpace(token))
							{
								context.Request.Headers.Add(HeaderNames.Authorization,
									new[] { BearerTokenFormatter.CreateToken(token) });
							}
						}
					}
				}

				await next.Invoke();

            });
		}
	}
}
