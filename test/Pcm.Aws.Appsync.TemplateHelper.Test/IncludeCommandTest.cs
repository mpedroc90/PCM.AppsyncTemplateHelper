using Xunit;
using System.IO;
using System;

namespace Pcm.Aws.Appsync.TemplateHelper.Test
{
    public class IncludeCommandTest :IDisposable
    {

        readonly FileSystemHelper fileSystemHelper;

        public IncludeCommandTest()
        {
            fileSystemHelper = new FileSystemHelper();
        }

        [Fact]
        public void SimpleInlcudeCommand ()
        {
                fileSystemHelper.CreateFile("file1.vm", @"this is a template 
                    ##:include file2.vm");
                fileSystemHelper.CreateFile("file2.vm", @"this is file2");

                Program.Main(new[] { "./" });

                string text = GetTextFromFile("file1.vm");
                FileTestAreEquals(@"this is a template
this is file2", text);
        }

        [Fact]
        public void SimpleInlcudeRecursive()
        {
            fileSystemHelper.CreateFile("file1.vm", @"this is a template 
                    ##:include file2.vm");
            fileSystemHelper.CreateFile("file2.vm", @"this is file2
##:include file3.vm");
            fileSystemHelper.CreateFile("file3.vm", @"this is file3");

            Program.Main(new[] { "./" });


            string text = GetTextFromFile("file1.vm");
            FileTestAreEquals(@"this is a template
                                this is file2
                                this is file3", text);
        }

        [Fact]
        public void FindFileInDiferentDirectory()
        {
            fileSystemHelper.CreateDirectory("files");
            fileSystemHelper.CreateFile("file1.vm", @"this is a template 
                    ##:include files/file2.vm");
            fileSystemHelper.CreateFile("files/file2.vm", @"this is file2
                    ##:include ../file3.vm");
            fileSystemHelper.CreateFile("file3.vm", @"this is file3");

            Program.Main(new[] { "./" });

            string text = GetTextFromFile("file1.vm");
            FileTestAreEquals(@"this is a template
                                this is file2
                                this is file3", text);
        }

        [Fact]
        public void MultipleIncludesinSameFiles()
        {
            //arrange
            fileSystemHelper.CreateFile("file1.vm", @"this is a template 
                    ##:include file2.vm
                    ##:include file2.vm");
            fileSystemHelper.CreateFile("file2.vm", @"this is file2");

            //action
            Program.Main(new[] { "./" });


            //assert
            string text = GetTextFromFile("file1.vm");
            FileTestAreEquals(@"this is a template
                                this is file2
                                this is file2", text);
        }

        private string GetTextFromFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
                return reader.ReadToEnd();
        }

        private void FileTestAreEquals(string expected , string current)
        {
            expected =expected.Trim();
            current = current.Trim();
            using (var expectedReader = new StringReader(expected))
            using (var currentReader = new StringReader(current))
            {
                string currentLine= null , expectedLine = null;
                while ((currentLine = currentReader.ReadLine()) != null & (expectedLine = expectedReader.ReadLine()) != null)
                    Assert.Equal(currentLine.Trim(), expectedLine.Trim());
                Assert.True( (expectedLine = expectedReader.ReadLine()) == null & (currentLine = currentReader.ReadLine()) == null); 
            }
        }

        public void Dispose()
        {
            fileSystemHelper.Dispose();
        }
    }
}
