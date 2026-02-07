using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SauceBucket.Models;
using SauceBucket.Helpers;

namespace SauceBucket.ViewModels.Widgets
{
    /// <summary>
    /// ViewModel for the left sidebar quick-access shortcuts.
    /// </summary>
    public class LeftSideBarViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<QuickAccessItem> QuickAccessItems { get; } = new();

        private bool _isEditing;
        public bool IsEditing { get => _isEditing; set { _isEditing = value; OnPropertyChanged(); } }

        public ICommand NavigateCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        public LeftSideBarViewModel()
        {
            // Sample shortcuts
            QuickAccessItems.Add(new QuickAccessItem { Label = "All Items", Target = "all" });
            QuickAccessItems.Add(new QuickAccessItem { Label = "Low Stock", Target = "low" });

            NavigateCommand = new DelegateCommand<string>(NavigateTo);
            AddCommand = new DelegateCommand(_ => AddShortcut());
            RemoveCommand = new DelegateCommand<object>(param =>
            {
                if (param is QuickAccessItem item) QuickAccessItems.Remove(item);
            });
        }

        private void NavigateTo(string target)
        {
            // TODO: Integrate with navigation logic
            System.Diagnostics.Debug.WriteLine($"Navigate to {target}");
        }

        private void AddShortcut()
        {
            QuickAccessItems.Add(new QuickAccessItem { Label = "New", Target = "new" });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string? n = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }
}