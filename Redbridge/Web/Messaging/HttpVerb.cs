using System;
namespace Redbridge.SDK
{
	public enum HttpVerb
	{
		Get,

		Put,

		Post,

		Delete,

		Patch,
	}

	public static class HttpVerbExtensions
	{
		public static string ToRequestVerb(this HttpVerb httpVerb)
		{
			switch (httpVerb)
			{
				case HttpVerb.Delete:
					return "DELETE";
				case HttpVerb.Get:
					return "GET";
				case HttpVerb.Post:
					return "POST";
				case HttpVerb.Put:
					return "PUT";
				case HttpVerb.Patch:
					return "PATCH";
				default:
					throw new RedbridgeException($"Unsupported http verb {httpVerb}");
			}
		}
	}
}
