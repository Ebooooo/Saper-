using System;
using System.Windows.Input;

namespace Saper.Data.Models
{
    /// <summary>
    /// Klasa odpowiedzialna za wywołanie komendy
    /// </summary>
    public class Command : ICommand
    {
        #region private fields
        private readonly Action<object> execute;
        private readonly Func<bool> canExecute;
        #endregion
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (this.canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public Command(Action execute)
            : this(execute, null) { }
        public Command(Action<object> execute)
            : this(execute, null) { }
        public Command(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = x => execute();
            this.canExecute = canExecute;
        }
        public Command(Action<object> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute();
        }

    }
    /// <summary>
    /// Generyczma klasa odpowiedzialna za wywołanie komendy
    /// </summary>
    /// <typeparam name="T">Typ parametru komendy</typeparam>
    public class Command<T> : ICommand
    {
        #region private fields
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;
        #endregion

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (this.canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public Command(Action<T> execute)
            : this(execute, (Func<T, bool>)null) { }
        public Command(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public Command(Action<T> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = x => canExecute();
        }
        public void Execute(object parameter)
        {
            this.execute((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute((T)parameter);
        }

    }
}
