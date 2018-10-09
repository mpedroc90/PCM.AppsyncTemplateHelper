using System.Collections.Generic;
using System.IO;

namespace Pcm.Aws.Appsync.TemplateHelper
{
    public static class StringReaderExtentions
    {
        public static IEnumerable<string> Lines(this StringReader stringReader)
        {
            string line = "";
            while ((line = stringReader.ReadLine()) != null)
                yield return line;
        }
    }
}
