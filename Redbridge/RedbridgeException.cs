﻿using System;

namespace Redbridge
{
	public class RedbridgeException : Exception
	{
		public RedbridgeException() { }

		public RedbridgeException(string message) : base(message) { }

		public RedbridgeException(string message, Exception inner) : base(message, inner) { }
	}
}
