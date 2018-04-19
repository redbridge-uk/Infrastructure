using System;
using Redbridge.SDK;
using Xamarin.Forms;
using System.Reactive.Linq;
using Redbridge.Validation;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
        private readonly IList<IDisposable> _disposables = new List<IDisposable>();

        public PageViewModel(ISchedulerService scheduler)
        {
            Scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
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

            AddToDisposables(RedbridgeThemeManager.Theme
                                 .Where(t => t != null)
                                 .ObserveOn(Scheduler.UiScheduler)
                                 .Subscribe((rt) =>
                                 {
                                    NavigationBarColour = rt.NavigationBarColour;
                                    NavigationBarTextColour = rt.NavigationTextColour;
                                 }));
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
                    OnPropertyChanged(nameof(NavigationBarColour));
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
                    OnPropertyChanged(nameof(BackgroundColour));
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
                    OnPropertyChanged(nameof(NavigationBarTextColour));
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
                    OnPropertyChanged(nameof(ShowNavigationBar));
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
                    OnPropertyChanged(nameof(ShowSearchBar));
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
                    OnPropertyChanged(nameof(ShowToolbar));
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
                    OnPropertyChanged(nameof(Title));
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
                    OnPropertyChanged(nameof(Icon));
                }
            }
        }

        public ValidationResultCollection Validate()
        {
            return OnValidate();
        }

        public async Task<bool> NavigateBack()
        {
            return await OnNavigateBack();
        }

        public void Dispose()
        {
            if (_disposables.Any())
            {
                foreach (var disposable in _disposables)
                    disposable.Dispose();

                _disposables.Clear();
            }

            OnDispose();
        }

        /// <summary>
        /// When this view model is disposed the given disposable will also be disposed
        /// </summary>
        protected void AddToDisposables(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        protected virtual void OnDispose() { }

        protected virtual ValidationResultCollection OnValidate()
        {
            return new ValidationResultCollection(true);
        }

        protected virtual async Task<bool> OnNavigateBack()
        {
            return await Task.FromResult(true);
        }
    }
}
