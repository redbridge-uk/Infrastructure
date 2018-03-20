namespace Redbridge.Messaging
{
	public interface IMailMessage
	{
		string Subject { get; }

		string Body { get; }

		string To { get; }

		string From { get; }
	}
}
