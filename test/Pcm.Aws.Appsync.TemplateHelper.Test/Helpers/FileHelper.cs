using System.IO;

namespace Pcm.Aws.Appsync.TemplateHelper.Test.Helpers
{
    class FileHelper
    {
        public static string GetTextFromFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
                return reader.ReadToEnd();
        }
    }
}
