using Pcm.Aws.Appsync.TemplateHelper.Commands;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pcm.Aws.Appsync.TemplateHelper
{
    public class FileProcessor
    {
        public FileInfo File { get; private set; }

        protected TextProccesor TextProccesor {get; private set;}

        public virtual string ProcessFile(string path)
        {
            Console.WriteLine($"Proccessing File {path}");
            Initialize(path);
            string text = ExtractTextFromFile(path);
            return TextProccesor.ProcessText(text);
        }

        private void Initialize(string path)
        {
            File = new FileInfo(path);
            IEnumerable<Command>  commands = new CommandContainer().GetCommands(new CommandBag
            {
                FileInfo = File,
            });

            TextProccesor = new TextProccesor(commands);
        }

        private string ExtractTextFromFile(string file)
        {
            using (StreamReader reader = new StreamReader(file))
                 return reader.ReadToEnd();
        }

    }
}
