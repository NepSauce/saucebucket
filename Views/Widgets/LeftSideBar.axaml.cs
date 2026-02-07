using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SauceBucket.ViewModels.Widgets;

namespace SauceBucket
{
    public partial class LeftSideBar : UserControl
    {
        public LeftSideBar()
        {
            InitializeComponent();
            DataContext = new LeftSideBarViewModel();
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
