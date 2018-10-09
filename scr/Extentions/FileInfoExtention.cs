using System;
using System.IO;

namespace Pcm.Aws.Appsync.TemplateHelper
{
    public static class FileInfoExtention
    {
        public static string GetRelativeDirectory(this FileInfo file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));
            var directory = Path.GetRelativePath(Directory.GetCurrentDirectory(), file.FullName);
            return directory.Remove(directory.Length - file.Name.Length);
        }
    }
}
