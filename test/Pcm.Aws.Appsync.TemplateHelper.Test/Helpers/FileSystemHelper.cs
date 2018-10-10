using System.IO;

namespace Pcm.Aws.Appsync.TemplateHelper.Test
{
    public class FileSystemHelper
    {
        public void DeleteDirectory(string path) => Directory.Delete(path, true);

        public void DeleteFile(string path) => File.Delete(path);

        public void CreateFile(string path, string content)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(content);
                streamWriter.Flush();
            }
        }
    }
}
