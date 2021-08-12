using System.IO;
using DependencyResolver;
using Microsoft.Extensions.DependencyInjection;
using URLParser.BLL.Contract;

#pragma warning disable SA1600 // Elements should be documented

namespace URLParser.PL.Console
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ResolverConfig().CreateServiceProvider();

            var service = serviceProvider.GetService<IService>();

            service.Run();

            System.Console.WriteLine(new StreamReader(ResolverConfig.ConfigurationRoot["DestinationFile"]).ReadToEnd());
            System.Console.ReadLine();
        }
    }
}
