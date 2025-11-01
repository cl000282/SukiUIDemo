using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.Ioc;
using SukiUIDemo.ViewModels;

namespace SukiUIDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // 手动设置 ViewModel，通过 Prism 容器解析确保依赖注入正常工作
            var app =   Application.Current as App;
            this.DataContext = app.Container.Resolve<MainWindowViewModel>();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}