/// <summary>
/// ICommand interface. Implemented by other Command objects.
/// Based off of design pattern: https://refactoring.guru/design-patterns/command/csharp/example
/// </summary>
public interface ICommand
{
    // Reference:
    // https://stackoverflow.com/questions/8788366/using-the-command-and-factory-design-patterns-for-executing-queued-jobs

    /// <summary>
    /// Execute the command.
    /// </summary>
    void Execute();
}