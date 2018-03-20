using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Redbridge.Xamarin.Forms.iOS;
using Redbridge.Forms;

[assembly: ExportRenderer(typeof(EntryCell), typeof(EntryCellSelectionRenderer))]

namespace Redbridge.Xamarin.Forms.iOS
{
	public class EntryCellSelectionRenderer : EntryCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var viewModel = item.BindingContext as IEntryCellViewModel;
			UITableViewCell cell = base.GetCell(item, reusableCell, tv);

			Disconnect(cell);

			if (viewModel != null)
			{
				cell.Accessory = viewModel.Accessories.ToiOSCellAssessory();

				if (viewModel.AllowCellSelection)
					cell.SelectionStyle = UITableViewCellSelectionStyle.Default;
				else
					cell.SelectionStyle = UITableViewCellSelectionStyle.None;

				if (cell.ContentView.Subviews.Length > 0)
				{
					var textField = cell.ContentView.Subviews[0] as UITextField;
					if (textField != null)
					{
                        textField.SecureTextEntry = viewModel.IsSecure;
                        textField.AutocapitalizationType = viewModel.AutoCapitalisationMode.ToiOSAutoCapitalisation();
						textField.ShouldChangeCharacters = (tf, range, replacementString) =>
						{
							if (viewModel != null )
								return viewModel.AllowTextEntry((int)range.Location, (int)range.Length, replacementString);

							return true;
						};

						if (!viewModel.AllowDirectEdit)
							textField.UserInteractionEnabled = false;

						textField.ClearButtonMode = viewModel.ClearButtonMode.ToiOSClearButtonMode();

                        if ( viewModel.PlaceholderIcon != Icon.None)
                        {
                            textField.LeftViewMode = UITextFieldViewMode.Always;

                            var mapper = new DefaultIconMapper();
                            var code = mapper.MapResource(viewModel.PlaceholderIcon, Color.Black, IconSize.Small);
                            UIImageView iconImage;
                            if (!string.IsNullOrWhiteSpace(code))
                                iconImage = new UIImageView(UIImage.FromFile(code));
                            else
                                iconImage = new UIImageView();

                            textField.LeftView = iconImage;
                        }
					}
				}
			}

			return cell;
		}

		private void Disconnect(UITableViewCell cell)
		{
            
			if (cell.ContentView.Subviews.Length > 0)
			{
				var textField = (UITextField)cell.ContentView.Subviews[0];
				textField.ShouldChangeCharacters = null;
			}
		}
	}
}
