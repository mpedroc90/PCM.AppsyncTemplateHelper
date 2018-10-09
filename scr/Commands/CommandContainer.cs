using System;
using System.Collections.Generic;
using System.Linq;

namespace Pcm.Aws.Appsync.TemplateHelper.Commands
{
    public class CommandContainer
    {
        public virtual IEnumerable<Command> GetCommands(CommandBag commandBag )
        {

            Type commandType = typeof(Command);
            return from type in commandType.Assembly.GetTypes()
                   where commandType.IsAssignableFrom(type) && !type.IsAbstract
                   select (Command)Activator.CreateInstance(type, commandBag);
        }
    }
}
