namespace Redbridge.Forms
{

	public class ActionSheetResponse<T>
	{
		public ActionSheetResponse(ActionSheetButtonResponse result, object owner)
		{
			Result = result;
			Owner = owner;
		}

		public ActionSheetButtonResponse Result { get; set; }
		public ActionSheetOption<T> Option { get; set; }
		public object Owner { get; set; }
	}

	public class ActionSheetResponse
	{
		public ActionSheetResponse(ActionSheetButtonResponse result, object owner)
		{
			Result = result;
			Owner = owner;
		}

		public ActionSheetButtonResponse Result { get; set; }
		public ActionSheetOption Option { get; set; }
		public object Owner { get; set; }
	}

}
