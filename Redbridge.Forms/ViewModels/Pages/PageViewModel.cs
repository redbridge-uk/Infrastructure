using System;
using Redbridge.SDK;
using Xamarin.Forms;
using System.Reactive.Linq;
using Redbridge.Validation;

namespace Redbridge.Forms
{
    public class PageViewModel : ViewModel, IPageViewModel
    {
        private string _pageTitle;
        private bool _showNavigationBar;
        private bool _showToolbar;
        private bool _showSearchBar;
        private ImageSource _pageIcon;
        private Color _navigationBarColour;
        private Color _navigationBarTextColour;
        private Color _backgroundColour;

        public PageViewModel(ISchedulerService scheduler)
        {
            if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));
            Scheduler = scheduler;
            Title = "Title";
            Toolbar = new ToolbarViewModel();
            SearchBar = new SearchBarViewModel();
            ShowSearchBar = false;
            ShowToolbar = true;

            if (RedbridgeThemeManager.HasTheme)
            {
                NavigationBarColour = RedbridgeThemeManager.Current.NavigationBarColour;
                NavigationBarTextColour = RedbridgeThemeManager.Current.NavigationTextColour;
            }

            RedbridgeThemeManager.Theme
                                 .Where(t => t != null)
                                 .ObserveOn(Scheduler.UiScheduler)
                                 .Subscribe((rt) =>
            {
                NavigationBarColour = rt.NavigationBarColour;
                NavigationBarTextColour = rt.NavigationTextColour;
            });
        }

        public ISchedulerService Scheduler { get; }

        public ToolbarViewModel Toolbar { get; private set; }
        public SearchBarViewModel SearchBar { get; private set; }

        public Color NavigationBarColour
        {
            get { return _navigationBarColour; }
            set
            {
                if (_navigationBarColour != value)
                {
                    _navigationBarColour = value;
                    OnPropertyChanged("NavigationBarColour");
                }
            }
        }

        public Color BackgroundColour
        {
            get { return _backgroundColour; }
            set
            {
                if (_backgroundColour != value)
                {
                    _backgroundColour = value;
                    OnPropertyChanged("BackgroundColour");
                }
            }
        }

        public Color NavigationBarTextColour
        {
            get { return _navigationBarTextColour; }
            set
            {
                if (_navigationBarTextColour != value)
                {
                    _navigationBarTextColour = value;
                    OnPropertyChanged("NavigationBarTextColour");
                }
            }
        }

        public bool ShowNavigationBar
        {
            get { return _showNavigationBar; }
            set
            {
                if (_showNavigationBar != value)
                {
                    _showNavigationBar = value;
                    OnPropertyChanged("ShowNavigationBar");
                }
            }
        }

        public bool ShowSearchBar
        {
            get { return _showSearchBar; }
            set
            {
                if (_showSearchBar != value)
                {
                    _showSearchBar = value;
                    OnPropertyChanged("ShowSearchBar");
                }
            }
        }

        public bool ShowToolbar
        {
            get { return _showToolbar; }
            set
            {
                if (_showToolbar != value)
                {
                    _showToolbar = value;
                    OnPropertyChanged("ShowToolbar");
                }
            }
        }

        public string Title
        {
            get { return _pageTitle; }
            set
            {
                if (_pageTitle != value)
                {
                    _pageTitle = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public ImageSource Icon
        {
            get { return _pageIcon; }
            set
            {
                if (_pageIcon != value)
                {
                    _pageIcon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }

        public ValidationResultCollection Validate()
        {
            return OnValidate();
        }

        protected virtual ValidationResultCollection OnValidate()
        {
            return new ValidationResultCollection(true);
        }

        public bool NavigateBack()
        {
            return OnNavigateBack();
        }

        protected virtual bool OnNavigateBack()
        {
            return true;
        }
    }
}
