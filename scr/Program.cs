using System.IO;
using System.Text.RegularExpressions;

namespace Pcm.Aws.Appsync.TemplateHelper
{
    class Program
    {

        static void  WriteInFile(string file, string text)
        {
            using (StreamWriter streamWriter = new StreamWriter(file))
            using (StringReader stringReader = new StringReader(text))
            {
                string line = null;
                while ((line = stringReader.ReadLine()) != null)
                    streamWriter.WriteLine(line);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        static void Main(string[] args)
        {
            var regex = new Regex("^*.(vm|graphql)");

            var fileProcessor = new FileProcessor();
            foreach (var file in Directory.GetFiles( args[0], "*", SearchOption.AllDirectories))
                if(regex.IsMatch(file))
                {
                    string text = fileProcessor.ProcessFile(file);
                    WriteInFile(file, text);
                }
        }
    }
}

