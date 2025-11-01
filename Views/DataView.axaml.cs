using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SukiUIDemo.Views
{
    public partial class DataView : UserControl
    {
        public DataView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}