namespace AppsyncHelper.Commands
{
    public abstract class Command
    {
        
       public Command(CommandBag commandBag )
        {
            CommandBag = commandBag;
        }

       protected CommandBag CommandBag { get; }

       public abstract bool IsCommand(string line);
       public abstract string ProcessLine(string line);
    }
}
