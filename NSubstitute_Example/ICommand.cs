using System;
namespace NSubstitute_Example
{
    public interface ICommand
    {
        void Execute();
        event EventHandler Executed;
    }
}