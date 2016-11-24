/**************************************8
 * 检测dll对应的.config文件，如果有变动重启系统服务
 * 2012-5-31
 * im@xingquan.org
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using System.IO;
using DW.Framework.Logger;

namespace DW.Framework.Configuration
{
    public abstract class XmlLoadBase
    {

        //private int countFileChangeEvent = 0, countTimerEvent = 0;
        private System.Threading.Timer timer;
        private const int TimeoutMillis = 500;

        protected string path;
        protected string file;

        protected XmlLoadBase(string path, string file)
        {
            this.path = path;
            this.file = file;
            Load();
            Run();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Run()
        {
            timer = new System.Threading.Timer(
         new System.Threading.TimerCallback(FileChanged_TimerChanged),
         null,
         System.Threading.Timeout.Infinite,
         System.Threading.Timeout.Infinite);

            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = this.path;//args[1];

            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName;
            // Only watch text files.
            watcher.Filter = this.file;

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            //Console.WriteLine("Press \'q\' to quit the sample.");
            //while (Console.Read() != 'q') ;
        }

        private void FileChanged_TimerChanged(object state)
        {
             Load();
        }

      
        protected abstract void Load();


        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            //countFileChangeEvent++;
            // Specify what is done when a file is changed, created, or deleted.
            //Log.Write("File: "+countFileChangeEvent + e.FullPath + " " + e.ChangeType);

            timer.Change(TimeoutMillis, System.Threading.Timeout.Infinite);
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Log.Write(string.Format("File: {0} renamed to {1}", e.OldFullPath, e.FullPath));
        }
    }
}
