namespace Redbridge.Forms
{
	public class AlertViewModel : ViewModel
	{ 
		public string Title { get; set; }
		public string Message { get; set; }
		public string AcceptMessage { get; set; }
        public string CancelMessage { get; set; } = "OK";
	}
	
}
