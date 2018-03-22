using Redbridge.SDK;
using Redbridge.Forms;
using Redbridge.Forms.ViewModels.Cells;
using Xamarin.Forms;

namespace TesterApp
{
    public class ReadonlyProfileTableViewModel : TableViewModel
    {
        private UserProfileExample _userProfile;
        private Command _addItemCommand;

        public ReadonlyProfileTableViewModel(INavigationService navigationService, ISchedulerService schedulerService)
            : base(navigationService, schedulerService)
        {
            Title = "31 South Drive";
            Intent = Xamarin.Forms.TableIntent.Form;
            BackgroundColour = Color.Black;
            _addItemCommand = new Command(async () =>
            {
                await Navigation.PushModalAsync<TestAddItemTableViewModel>();
            });
            _userProfile = new UserProfileExample()
            {
                Firstname = "Jacob",
                EnableNotifications = true,
            };

            var areas = AddSection("Areas", false);
            var environmentCell = areas.AddIconCell(Redbridge.Forms.Icon.School, "Environment", indicators: CellIndicators.Disclosure);
            environmentCell.CellHeight = TableCellHeight.Medium;
            environmentCell.BackgroundColour = Color.FromHex("E27D60");
            environmentCell.DisplayMode = IconCellViewMode.TitleOnly;
            environmentCell.TextColour = Color.White;
            environmentCell.IconColour = Color.White;

            var utilitiesCell = areas.AddIconCell(Redbridge.Forms.Icon.Settings, "Utilities", indicators: CellIndicators.Disclosure);
            utilitiesCell.CellHeight = TableCellHeight.Medium;
            utilitiesCell.BackgroundColour = Color.FromHex("085DCB");
            utilitiesCell.DisplayMode = IconCellViewMode.TitleOnly;
            utilitiesCell.TextColour = Color.White;
            utilitiesCell.IconColour = Color.White;

            var propertyCell = areas.AddIconCell(Redbridge.Forms.Icon.Home, "Property", indicators: CellIndicators.Disclosure);
            propertyCell.CellHeight = TableCellHeight.Medium;
            propertyCell.BackgroundColour = Color.FromHex("E8A87C");
            propertyCell.DisplayMode = IconCellViewMode.TitleOnly;
            propertyCell.TextColour = Color.White;
            propertyCell.IconColour = Color.White;

            var crimeCell = areas.AddIconCell(Redbridge.Forms.Icon.Police, "Crime", indicators: CellIndicators.Disclosure);
            crimeCell.CellHeight = TableCellHeight.Medium;
            crimeCell.BackgroundColour = Color.FromHex("C38D9E");
            crimeCell.DisplayMode = IconCellViewMode.TitleOnly;
            crimeCell.TextColour = Color.White;
            crimeCell.IconColour = Color.White;

            var valuationCell = areas.AddIconCell(Redbridge.Forms.Icon.Money, "Valuation", indicators: CellIndicators.Disclosure);
            valuationCell.CellHeight = TableCellHeight.Medium;
            valuationCell.BackgroundColour = Color.FromHex("41B3A3");
            valuationCell.DisplayMode = IconCellViewMode.TitleOnly;
            valuationCell.TextColour = Color.White;
            valuationCell.IconColour = Color.White;

            areas.AddCell(new IconTextCellViewModel(Redbridge.Forms.Icon.Hospital, "Some text", "", CellIndicators.Disclosure));
            Toolbar.Add(new TextToolbarItemViewModel() { Text = "Add", Command = _addItemCommand });
        }
    }
}
