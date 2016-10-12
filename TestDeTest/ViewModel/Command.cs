namespace TestDeTest.ViewModel
{
    using System;
    using System.Linq;
    using System.Windows.Input;

    public class Command : ICommand
    {
        private readonly Action<object> _executeAction;
        private readonly Func<object, bool> _canExecuteFunc;

        public Command(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                _executeAction(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc(parameter);
        }

        public void UpdateCanExecute(Action action, object parameter = null)
        {
            var beforeAction = CanExecute(parameter);
            action();
            var afterAction = CanExecute(parameter);

            if (beforeAction != afterAction)
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public static class ActionExtensions
    {
        public static void UpdateCanExecute(this Action action, params Command[] commands)
        {
            var count = commands.Length;
            switch (count)
            {
                case 0:
                    return;
                case 1:
                    commands[0].UpdateCanExecute(action);
                    return;
            }

            UpdateCanExecute(() => UpdateCanExecute(action, commands[count - 1]), commands.Take(count - 1).ToArray());
        }
    }
}