using System.IO;
using Xunit;

namespace Pcm.Aws.Appsync.TemplateHelper.Test.Helpers
{
    public static class FileAssert {
        public static void ContentMatch(string path, string current)
        {
            var expected = FileHelper.GetTextFromFile(path).Trim();
            current = current.Trim();
            using (var expectedReader = new StringReader(expected))
            using (var currentReader = new StringReader(current))
            {
                string currentLine = null, expectedLine = null;
                while ((currentLine = currentReader.ReadLine()) != null & (expectedLine = expectedReader.ReadLine()) != null)
                    Assert.Equal(currentLine.Trim(), expectedLine.Trim());
                Assert.True((expectedLine = expectedReader.ReadLine()) == null & (currentLine = currentReader.ReadLine()) == null);
            }
        }
    }
}
