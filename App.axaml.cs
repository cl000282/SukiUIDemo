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
       // public IContainerProvider Container { get; private set; }
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
           // this.Container = base.Container;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {        
            
            // 注册 ViewModel
            containerRegistry.Register<MainWindowViewModel>();
            containerRegistry.Register<DataTableViewModel>();
            // 注册视图
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<ButtonsView>();
            containerRegistry.RegisterForNavigation<InputsView>();
            containerRegistry.RegisterForNavigation<DataView>();
            containerRegistry.RegisterForNavigation<NavigationView>();
            containerRegistry.RegisterForNavigation<DataTableView>();
            

        }

        protected override void OnInitialized()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = CreateShell();//Container.Resolve<MainWindow>();

                // 设置默认视图
                var regionManager = Container.Resolve<IRegionManager>();
                regionManager.RequestNavigate("ContentRegion", "HomeView");
            }
            base.OnInitialized();
        }

        protected override Avalonia.Controls.Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}