using Xunit;
using System.IO;

namespace Pcm.Aws.Appsync.TemplateHelper.Test
{
    public class IncludeCommandTest
    {

        readonly FileSystemHelper fileSystemHelper = new FileSystemHelper(); 
        




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


            fileSystemHelper.DeleteFile("file1.vm");
            fileSystemHelper.DeleteFile("file2.vm");

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

            fileSystemHelper.DeleteFile("file1.vm");
            fileSystemHelper.DeleteFile("file2.vm");
            fileSystemHelper.DeleteFile("file3.vm");

        }

        [Fact]
        public void FindFileInDiferentDirectory()
        {
            fileSystemHelper.CreateFile("file1.vm", @"this is a template 
                    ##:include files/file2.vm");

            Directory.CreateDirectory("files");
            fileSystemHelper.CreateFile("files/file2.vm", @"this is file2
##:include ../file3.vm");
            fileSystemHelper.CreateFile("file3.vm", @"this is file3");

            Program.Main(new[] { "./" });

            string text = GetTextFromFile("file1.vm");

            FileTestAreEquals(@"this is a template
this is file2
this is file3", text);

            fileSystemHelper.DeleteFile("file1.vm");
            fileSystemHelper.DeleteFile("files/file2.vm");
            fileSystemHelper.DeleteDirectory("files");
            fileSystemHelper.DeleteFile("file3.vm");

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

        
            // teardown
            fileSystemHelper.DeleteFile("file1.vm");
            fileSystemHelper.DeleteFile("file2.vm");
  

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
    }
}
