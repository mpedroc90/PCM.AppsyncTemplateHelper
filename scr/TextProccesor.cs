using Pcm.Aws.Appsync.TemplateHelper.Commands;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pcm.Aws.Appsync.TemplateHelper
{
    public class TextProccesor
    {
        IEnumerable<Command> _commands;
        public TextProccesor(IEnumerable<Command> commands)
        {
            _commands = commands;
        }
        public virtual string ProcessText(string text)
        {
            StringBuilder sb = new StringBuilder();
            StringReader stringReader = new StringReader(text);

            foreach (var line in stringReader.Lines())
            {
                Command command = _commands.Where(c => c.IsCommand(line)).FirstOrDefault();
                if (null == command)
                {
                    sb.AppendLine(line);
                    continue;
                }

                sb.Append(command.ProcessLine(line));
            }

            return sb.ToString();
        }
    }
}
