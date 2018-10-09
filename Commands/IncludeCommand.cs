using System.IO;
using System.Linq;

namespace AppsyncHelper.Commands
{
    public class IncludeCommand : Command
    {
        private const string command = "##:include";

        public IncludeCommand(CommandBag commandBag) : base(commandBag)
        {}

        public override bool IsCommand(string line)
            => line.Trim().Contains(command);
        

        public override string ProcessLine(string line)
        {
            string relativePath = GetRelativePath(line);
            return new FileProcessor().ProcessFile(relativePath);
        }

        protected virtual string GetRelativePath(string line)
        {
            var directory = CommandBag.FileInfo.GetRelativeDirectory();
            string path = line.Trim().Substring(command.Length).Trim();
            return Path.Combine(directory, path);
        }
    }
}
