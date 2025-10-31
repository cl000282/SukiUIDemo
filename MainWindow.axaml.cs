using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SukiUIDemo
{
    public partial class MainWindow : Window
    {
        private ContentControl? _contentRegion;

        public MainWindow()
        {
            InitializeComponent();
            
            // 初始化显示首页
            NavigateToHome();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _contentRegion = this.FindControl<ContentControl>("ContentRegion");
        }

        private void NavigateToHome(object? sender = null, RoutedEventArgs? e = null)
        {
            _contentRegion!.Content = new Views.HomeView();
        }

        private void NavigateToButtons(object? sender = null, RoutedEventArgs? e = null)
        {
            _contentRegion!.Content = new Views.ButtonsView();
        }

        private void NavigateToInputs(object? sender = null, RoutedEventArgs? e = null)
        {
            _contentRegion!.Content = new Views.InputsView();
        }

        private void NavigateToData(object? sender = null, RoutedEventArgs? e = null)
        {
            _contentRegion!.Content = new Views.DataView();
        }

        private void NavigateToNavigation(object? sender = null, RoutedEventArgs? e = null)
        {
            _contentRegion!.Content = new Views.NavigationView();
        }

        private void NavigateToDataTable(object? sender = null, RoutedEventArgs? e = null)
        {
            _contentRegion!.Content = new Views.DataTableView();
        }
    }
}