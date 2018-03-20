﻿using System;
using System.Runtime.Serialization;

namespace Redbridge.Messaging
{
	[DataContract]
	public class EmailMessageData : IMailMessage
	{
		public EmailMessageData() { }

		public EmailMessageData(string from, string to, string subject, string body)
		{
			From = from;
			To = to;
			Subject = subject;
			Body = body;
		}

		[DataMember]
		public string Subject { get; set; }

		[DataMember]
		public string Body { get; set; }

		[DataMember]
		public string To { get; set; }

		[DataMember]
		public string From { get; set; }

		[DataMember]
		public DateTime Sent { get; set; }
	}
}
