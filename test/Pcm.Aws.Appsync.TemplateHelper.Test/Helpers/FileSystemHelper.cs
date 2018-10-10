using System;
using System.Collections.Generic;
using System.IO;

namespace Pcm.Aws.Appsync.TemplateHelper.Test
{

    /// <summary>
    /// This allows to create with files and directories . in the moment that object dispose , all items have been created with it are delete. 
    /// </summary>
    public class FileSystemHelper : IDisposable
    {
        private enum ContentType {
            File,
            Directory
        }
        private struct Content
        {
            public string Path { get; set; }
            public ContentType Type { get; set; }
        }

        IList<Content> contentCreatedList = new List<Content>();

        public void DeleteDirectory(string path) =>
            DeleteAction(() => Directory.Delete(path), path, ContentType.File);
        public void DeleteFile(string path) =>
             DeleteAction(() => File.Delete(path), path, ContentType.File);

        public void CreateFile(string path, string content)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(content);
                streamWriter.Flush();
            }
            AddContentToList(path, ContentType.File);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
            AddContentToList(path, ContentType.Directory);
        }

        private void AddContentToList(string path, ContentType type)
        {
           
            contentCreatedList.Add(new Content
            {
                Path = path,
                Type = type
            });
        }

        private void DeleteAction(Action delete, string path, ContentType type)
        {
            delete();
            contentCreatedList.Remove(new Content
            {
                Path = path,
                Type = type
            });
        }

        public void Dispose()
        {
            var deleteActions = new Dictionary<ContentType, Action<string>>
            {
                {ContentType.Directory , (path) =>{ if(Directory.Exists(path)) Directory.Delete(path, true); } },
                {ContentType.File ,  (path) => { if(File.Exists(path)) File.Delete(path); } }
            };
 
             foreach(Content content in contentCreatedList)
             {
                var delete = deleteActions[content.Type]; 
                delete(content.Path);
             }
        }
    }
}
