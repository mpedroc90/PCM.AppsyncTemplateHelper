using System.Collections.Generic;
using System.IO;

namespace AppsyncHelper
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
