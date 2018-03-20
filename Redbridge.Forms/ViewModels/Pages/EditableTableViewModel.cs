using System;
using Redbridge.SDK;
using Redbridge.Linq;
using Xamarin.Forms;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Redbridge.Validation;

namespace Redbridge.Forms
{
    public class EditableTableViewModel : TableViewModel
    {
        private IToolbarItemViewModel _editBarItem;
        private readonly Command _toggleEditModeCommand;
        private BehaviorSubject<bool> _editMode = new BehaviorSubject<bool>(false);
        readonly IAlertController _alerts;

        public EditableTableViewModel(IAlertController alerts, INavigationService navigationService, ISchedulerService schedulerService)
            : base(navigationService, schedulerService)
        {
            
            Intent = TableIntent.Form; // More common?
            _alerts = alerts;
            _toggleEditModeCommand = new Command((obj) => ToggleEditMode());
            _editBarItem = new TextToolbarItemViewModel("Edit", _toggleEditModeCommand);

            _editMode.ObserveOn(schedulerService.UiScheduler).Subscribe((em) =>
            {
                _editBarItem.Text = em ? DoneButtonCaption : EditButtonCaption;
                Sections.ForEach(s => s.EditMode = em);
            });

            Toolbar.Add(_editBarItem);
        }

        protected virtual string EditButtonCaption => "Edit";
        protected virtual string DoneButtonCaption => "Done";

        public IObservable<bool> EditModeObservable => _editMode;

        public void ToggleEditMode()
        {
            // if we are in edit mode, then validate that we can exit it ok
            if ( EditMode )
            {
                var validation = OnValidate();
                if (!validation.Success)
                {
                    
                    return; // Prevent exiting edit mode
                }
            }
            
            _editMode.OnNext(!EditMode);
        }

        protected virtual ValidationResultCollection OnValidate ()
        {
            return new ValidationResultCollection(true);
        }

        public bool EditMode
        {
            get { return _editMode.Value; }
        }
    }
}
