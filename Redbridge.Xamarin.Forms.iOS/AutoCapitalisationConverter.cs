using System;
using Redbridge.Forms;
using UIKit;

namespace Redbridge.Xamarin.Forms.iOS
{
	public static class AutoCapitalisationConverter
	{
		public static UITextAutocapitalizationType ToiOSAutoCapitalisation (this AutoCapitalisationMode mode)
		{
			switch (mode)
			{
				case AutoCapitalisationMode.Sentence:
					return UITextAutocapitalizationType.Sentences;
				case AutoCapitalisationMode.Word:
					return UITextAutocapitalizationType.Words;
				case AutoCapitalisationMode.All:
					return UITextAutocapitalizationType.AllCharacters;
				default:
					return UITextAutocapitalizationType.None;
			}
		}
	}
}
