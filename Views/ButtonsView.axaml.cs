using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SukiUIDemo.Views
{
    public partial class ButtonsView : UserControl
    {
        public ButtonsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                System.Console.WriteLine($"按钮点击: {button.Content}");
            }
        }
    }
}