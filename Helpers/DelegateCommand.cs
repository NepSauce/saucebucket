using System;
using System.Windows.Input;

namespace SauceBucket.Helpers
{
    /// <summary>
    /// Minimal ICommand implementation for MVVM.
    /// </summary>
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool>? _canExecute;

        public DelegateCommand(Action<T> execute, Func<T, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute((T?)parameter!);
        }

        public void Execute(object? parameter) => _execute((T?)parameter!);

        public event EventHandler? CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class DelegateCommand : DelegateCommand<object?>
    {
        public DelegateCommand(Action<object?> execute, Func<object?, bool>? canExecute = null) : base(execute, canExecute) { }
    }
}
