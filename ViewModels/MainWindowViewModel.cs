using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System.Windows.Input;

namespace SukiUIDemo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        
        public ICommand NavigateToHomeCommand { get; }
        public ICommand NavigateToButtonsCommand { get; }
        public ICommand NavigateToInputsCommand { get; }
        public ICommand NavigateToDataCommand { get; }
        public ICommand NavigateToDataTableCommand { get; }
        public ICommand NavigateToNavigationCommand { get; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            
            NavigateToHomeCommand = new DelegateCommand(() => NavigateToHome());
            NavigateToButtonsCommand = new DelegateCommand(() => NavigateToButtons());
            NavigateToInputsCommand = new DelegateCommand(() => NavigateToInputs());
            NavigateToDataCommand = new DelegateCommand(() => NavigateToData());
            NavigateToDataTableCommand = new DelegateCommand(() => NavigateToDataTable());
            NavigateToNavigationCommand = new DelegateCommand(() => NavigateToNavigation());
        }

        public void NavigateToHome()
        {
            _regionManager.RequestNavigate("ContentRegion", "HomeView");
        }

        public void NavigateToButtons()
        {
            _regionManager.RequestNavigate("ContentRegion", "ButtonsView");
        }

        public void NavigateToInputs()
        {
            _regionManager.RequestNavigate("ContentRegion", "InputsView");
        }

        public void NavigateToData()
        {
            _regionManager.RequestNavigate("ContentRegion", "DataView");
        }

        public void NavigateToDataTable()
        {
            _regionManager.RequestNavigate("ContentRegion", "DataTableView");
        }

        public void NavigateToNavigation()
        {
            _regionManager.RequestNavigate("ContentRegion", "NavigationView");
        }
    }
}