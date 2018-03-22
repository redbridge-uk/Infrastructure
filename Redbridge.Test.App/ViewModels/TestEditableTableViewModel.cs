using System;
using Redbridge.SDK;
using Redbridge.Forms;
using Xamarin.Forms;
using Redbridge.Identity;

namespace TesterApp
{

    public class TestEditableTableViewModel : EditableTableViewModel
	{
        private readonly IAuthenticationClientFactory _signInFactory;
        private UserProfileExample _userProfile;
        private Command _loginGoogle;

		public TestEditableTableViewModel(IAuthenticationClientFactory signInFactory, IAlertController alerts, INavigationService navigationService, ISchedulerService scheduler) 
            : base(alerts, navigationService, scheduler) 
		{
            _signInFactory = signInFactory;

            _userProfile = new UserProfileExample()
            {
                Firstname = "John",
                EnableNotifications = true,
            };

            _loginGoogle = new Command(() => 
            {
                var signin = _signInFactory.Create("Google");
                signin.BeginLoginAsync();
            });

            var item1 = new TestItem() { DisplayText = "Smith" };
            var item2 = new TestItem() { DisplayText = "Smythe" };
            var categories = new[] { item1, item2 };

            Title = "Editable Table Test";
            var general = AddSection("General");
            general.AddCell(new SelectCellViewModel<TestItem, Guid>() { Label = "Category", PlaceholderText = "Select category", ItemSource = categories });
            general.AddCell(new DateTimeCellViewModel() { Label = "Entry date", PlaceholderText = "Select entry date" });
			general.AddCell(new SelectCellViewModel<TestItem, Guid>() { Label = "Other category", PlaceholderText = "Select other category", ItemSource = categories, SelectedItem = item2 });

			var detail = AddSection("Detail");
            detail.AddCell(new TextEntryCellViewModel() { AutoCapitalisationMode = AutoCapitalisationMode.All });
            detail.AddTextEntryCell(_userProfile, (up)=> up.Firstname, "Firstname", "Enter first name");
			detail.AddTextEntryCell(_userProfile, (up) => up.Surname, "Surname", "Enter surname", Redbridge.Forms.Icon.Account);
            detail.AddDateTimeCell(_userProfile, (up) => up.DateOfBirth, "Birthday", "Enter birthday");
            var cell = detail.AddSwitchCell(_userProfile, (up) => up.EnableNotifications, "Notifications");

            var logins = AddSection("Logins");
            logins.AddCell(new TextCellViewModel() { Command = _loginGoogle, Text = "Google me" });

            var list = AddSection("List");

		}
	}
}
