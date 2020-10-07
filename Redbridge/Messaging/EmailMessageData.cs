﻿using System;
using System.Runtime.Serialization;
 using Redbridge.Data;

 namespace Redbridge.Messaging
{
	[DataContract]
	public class EmailMessageData : IMailMessage
	{
		public EmailMessageData() { }

		public EmailMessageData(string from, string to, string subject, string body, BodyType bodyType = BodyType.Html)
		{
			From = from;
			To = to;
			Subject = subject;
			Body = body;
            BodyType = bodyType;
        }

		[DataMember]
		public string Subject { get; set; }

		[DataMember]
		public string Body { get; set; }

        [DataMember]
        public BodyType BodyType { get; set; }

		[DataMember]
		public string To { get; set; }

		[DataMember]
		public string From { get; set; }

		[DataMember]
		public DateTime Sent { get; set; }
	}
}
