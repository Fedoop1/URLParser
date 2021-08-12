using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using URLParser.BLL.Contract;
using URLParser.BLL.Implementation;
using URLParser.BLL.Implementation.Entities;
using URLParser.DAL.Contract;
using URLParser.DAL.Implementation;

namespace DependencyResolver
{
    /// <summary>
    /// Configuration root class that resolve all necessary dependencies.
    /// </summary>
    public class ResolverConfig
    {
        /// <summary>
        /// Gets the configuration root.
        /// </summary>
        /// <value>
        /// The configuration root instance.
        /// </value>
        public static IConfigurationRoot ConfigurationRoot { get; } = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        /// <summary>
        /// Creates and return the service provider.
        /// </summary>
        /// <returns>Service provider instance.</returns>
        public IServiceProvider CreateServiceProvider()
        {
            string sourceFilePath = GetValidPath(ConfigurationRoot["sourceFile"]);
            string destinationFilePath = GetValidPath(ConfigurationRoot["destinationFile"]);
            string loggerConfigurationFilePath = GetValidPath(ConfigurationRoot["LoggerConfiguration"]);

            return new ServiceCollection()
                .AddSingleton(LogManager.LoadConfiguration(loggerConfigurationFilePath))
                .AddTransient<ILogger, Logger>(provider => provider.GetService<LogFactory>().GetLogger("Console"))
                .AddTransient<ISerializer<IEnumerable<Url>, string>, XmlSerializer>()
                .AddTransient<IDeserializer<string, IEnumerable<Url>>, UrlDeserializer>()
                .AddScoped<IDataLoader<string>, FileSystemStorage>(provider => new FileSystemStorage(sourceFilePath, destinationFilePath))
                .AddScoped<IDataSaver<string>, FileSystemStorage>(provider => new FileSystemStorage(sourceFilePath, destinationFilePath))
                .AddSingleton<IService, UrlParserService<string>>()
                .BuildServiceProvider();
        }

        private static string GetValidPath(string path) => Path.Combine(Environment.CurrentDirectory, path);
    }
}
