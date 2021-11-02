using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using System.ServiceProcess;
using Topshelf;
using Business.DependencyResolvers.Autofac;
using System.Data;
using System.Data.SqlClient;

namespace FileWatcherService
{
    class Program
    {
        static void Main(string[] args)
        {

            //ServiceProvider serviceProvider = new ServiceCollection()
            //                               .AddScoped<IFileWatcherService, FileWatcherManager>()
            //                               .AddScoped<IFileDal,DPFileDal>()
            //                               .AddScoped<IConfiguration>()
            //                               .BuildServiceProvider();


            //IFileWatcherService watcherService = serviceProvider.GetService<IFileWatcherService>();
      
            //ContainerBuilder containerBuilder = new ContainerBuilder();

            //containerBuilder.RegisterType<FileWatcher>().AsSelf().InstancePerLifetimeScope();
            //containerBuilder.RegisterType<FileWatcherManager>().As<IFileWatcherService>().InstancePerLifetimeScope();
            //containerBuilder.RegisterType<DPFileDal>().As<IFileDal>().InstancePerLifetimeScope();

            //IContainer container = containerBuilder.Build();

            //ContainerBuilder containerBuilder = new ContainerBuilder();

            //containerBuilder.RegisterType<DPFileDal>().AsSelf().InstancePerLifetimeScope();

            //IContainer container = containerBuilder.Build();



            HostFactory.Run(hostConfig =>
            {
                
                hostConfig.Service<FileWatcher>(serviceConfig =>
                {
                    serviceConfig.ConstructUsing(fileWatcher => new FileWatcher());
                    serviceConfig.WhenStarted(fileWatcher => fileWatcher.Start());
                    serviceConfig.WhenStopped(fileWatcher => fileWatcher.Stop());


                });
                hostConfig.RunAsLocalSystem();
                hostConfig.SetServiceName("WatcherProject");
                hostConfig.SetDisplayName("Watcher Project");
            });
        }
    }
}

