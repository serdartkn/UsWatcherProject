using System;
using Topshelf;

namespace FileWatcherService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(hostConfig =>
            {
                hostConfig.Service<FileWatcher>(serviceConfig =>
                {
                    serviceConfig.ConstructUsing(fileWatcher => new FileWatcher());
                    serviceConfig.WhenStarted(fileWatcher => fileWatcher.Start());
                    serviceConfig.WhenStopped(fileWatcher => fileWatcher.Stop());
                });
                hostConfig.RunAsLocalSystem();
                hostConfig.SetServiceName("FileWatcherProject");
                hostConfig.SetDisplayName("File Watcher Project");
            });            
        }
    }
}