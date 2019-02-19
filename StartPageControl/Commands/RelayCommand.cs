using System;
using System.Windows.Input;

namespace StartPageControl.Commands
{
  /// <summary>
  /// Generic Relay command
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <seealso cref="System.Windows.Input.ICommand" />
  public class RelayCommand<T> : ICommand
  {
    private readonly Predicate<T> _canExecute;
    private readonly Action<T> _execute;

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
    /// </summary>
    /// <param name="canExecute">The can execute.</param>
    /// <param name="execute">The execute.</param>
    public RelayCommand(Predicate<T> canExecute, Action<T> execute)
    {
      this._canExecute = canExecute;
      this._execute = execute;
    }

    /// <summary>
    /// Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    public event EventHandler CanExecuteChanged
    {
      add => CommandManager.RequerySuggested += value;
      remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    /// <returns>
    /// true if this command can be executed; otherwise, false.
    /// </returns>
    public bool CanExecute(object parameter)
    {
      return _canExecute?.Invoke((T)parameter) ?? true;
    }

    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    public void Execute(object parameter)
    {
      _execute((T)parameter);
    }

  }
  /// <summary>
  /// non generic version of RelayCommand
  /// </summary>
  /// <seealso cref="System.Windows.Input.ICommand" />
  public class RelayCommand : RelayCommand<object>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand"/> class.
    /// </summary>
    /// <param name="execute">The execute.</param>
    public RelayCommand(Action execute)
      : this(null, execute)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand"/> class.
    /// </summary>
    /// <param name="canExecute">The can execute.</param>
    /// <param name="execute">The execute.</param>
    public RelayCommand(Func<bool> canExecute, Action execute)
      : base(param => canExecute?.Invoke() ?? true, param => execute())
    {
    }
  }
}
