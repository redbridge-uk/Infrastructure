using Redbridge.Forms;

namespace TesterApp
{
    public class CategoryData : IDisplayText
	{
		public string Name { get; set; }
		public string ImageSource { get; set; }
        string IDisplayText.DisplayText => Name;
    }
	
}
