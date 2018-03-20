using System;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public interface IView
	{ 
		object BindingContext { get; set; }
	}
	
}
