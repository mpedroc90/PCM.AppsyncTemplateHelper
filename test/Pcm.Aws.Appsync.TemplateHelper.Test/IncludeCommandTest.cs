using Xunit;
using System;
using Pcm.Aws.Appsync.TemplateHelper.Test.Helpers;

namespace Pcm.Aws.Appsync.TemplateHelper.Test
{
    public class IncludeCommandTest :IDisposable
    {

        readonly FileSystemHelper fileSystemHelper;

        //set up
        public IncludeCommandTest()
        {
            fileSystemHelper = new FileSystemHelper();
        }

        [Fact]
        public void SimpleInlcudeCommand ()
        {
            //arrange
            fileSystemHelper.CreateFile("file1.vm", @"this is a template 
                ##:include file2.vm");
            fileSystemHelper.CreateFile("file2.vm", @"this is file2");

            //action
            Program.Main(new[] { "./" });
            
            //assert
            FileAssert.ContentMatch("file1.vm",@"this is a template
                                    this is file2");
        }

        [Fact]
        public void SimpleInlcudeRecursive()
        {
            //arrange
            fileSystemHelper.CreateFile("file1.vm", @"this is a template 
                    ##:include file2.vm");
            fileSystemHelper.CreateFile("file2.vm", @"this is file2
                    ##:include file3.vm");
            fileSystemHelper.CreateFile("file3.vm", @"this is file3");

            //action
            Program.Main(new[] { "./" });

            //assert
            FileAssert.ContentMatch("file1.vm",@"this is a template
                                this is file2
                                this is file3");
        }

        [Fact]
        public void FindFileInDiferentDirectory()
        {
            //arrange
            fileSystemHelper.CreateDirectory("files");
            fileSystemHelper.CreateFile("file1.vm", @"this is a template 
                    ##:include files/file2.vm");
            fileSystemHelper.CreateFile("files/file2.vm", @"this is file2
                    ##:include ../file3.vm");
            fileSystemHelper.CreateFile("file3.vm", @"this is file3");

            //action
            Program.Main(new[] { "./" });
            
            //assert
            FileAssert.ContentMatch("file1.vm", @"this is a template
                                this is file2
                                this is file3");
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
            FileAssert.ContentMatch("file1.vm" , @"this is a template
                                this is file2
                                this is file2");
        }
        
        //tear down
        public void Dispose()
        {
            fileSystemHelper.Dispose();
        }
    }
}
