using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Prism.DryIoc;
using Prism.Ioc;
using SukiUIDemo.Views;
using SukiUIDemo.ViewModels;
using Prism.Navigation.Regions;

namespace SukiUIDemo
{
    public partial class App : PrismApplication
    { 

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize(); 
        }

        protected override void OnInitialized()
        {      
            base.OnInitialized();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = Container.Resolve<MainWindow>();
                var regionManager = Container.Resolve<IRegionManager>();
                regionManager.RequestNavigate("ContentRegion", "HomeView");
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册 ViewModel
            containerRegistry.Register<DataTableViewModel>();
            containerRegistry.Register<MainWindowViewModel>();

            // 注册视图
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<ButtonsView>();
            containerRegistry.RegisterForNavigation<InputsView>();
            containerRegistry.RegisterForNavigation<DataView>();
            containerRegistry.RegisterForNavigation<NavigationView>();
            containerRegistry.RegisterForNavigation<DataTableView>(); 
        }

        protected override Avalonia.Controls.Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}