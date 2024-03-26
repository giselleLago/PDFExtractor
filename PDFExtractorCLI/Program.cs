using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PDFExtractors.Extractor;
using PDFExtractors.Models;

namespace PDFExtractorCLI
{
    public static class Program
    {
        static int Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices((hostContext, services) =>
                 {
                     ConfigureServices(services);
                 })
                 .Build();

            var app = new CommandLineApplication();
            app.HelpOption();

            var pathArg = app.Argument("path", "Path to PDF file").IsRequired();

            app.OnExecute(() =>
            {
                try
                {
                    var services = host.Services.CreateScope().ServiceProvider;
                    var extractor = services.GetRequiredService<Extractor>();
                    var pages = extractor.ExtractData(pathArg.Value);

                    for (var i = 0; i < pages.Count; i++)
                    {
                        Console.WriteLine($"\nPrinting page {i + 1} info: ");
                        foreach (var field in pages[i])
                        {
                            Console.WriteLine($"{field.Name}: {field.Value}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong: " + ex.Message);
                }
            });

            return app.Execute(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var regexConfig = ReadRegexConfig();
            services.AddSingleton(regexConfig);
            services.AddTransient<IPDFReader, PDFReader>();
            services.AddTransient<IDataExtractorEngine, RegexDataExtractorEngine>();
            services.AddTransient<Extractor>();
        }

        private static RegexConfig ReadRegexConfig()
        {
            string json = File.ReadAllText(ConfigPath);
            var config = JsonConvert.DeserializeObject<RegexConfig>(json);
            if (config == null)
            {
                throw new Exception("Unable to read regex config file.");
            }
            return config;
        }

        private const string ConfigPath = ".\\Config\\RegexConfig.json";
    }
}
