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

            var pathArg = app.Argument("path", "Path to PDF file");


            app.OnExecute(() =>
            {
                try
                {
                    var serviceScope = host.Services.CreateScope();
                    var services = serviceScope.ServiceProvider;

                    var extractor = services.GetRequiredService<Extractor>();
                    var extractedData = extractor.ExtractData(pathArg.Value, ReadRegexConfig());

                    var flightNumber = 1;
                    foreach (var data in extractedData)
                    {
                        Console.WriteLine($"\nPrinting flight {flightNumber} info: ");
                        foreach (var extractedField in data)
                        {
                            Console.WriteLine($"{extractedField.Value.Name} : {extractedField.Value.Value}");
                        }

                        flightNumber++;
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
            services.AddTransient<IPDFReader, PDFReader>();
            services.AddTransient<IDataExtractorEngine, RegexDataExtractorEngine>();
            services.AddTransient<Extractor>();
        }

        private static RegexConfig? ReadRegexConfig()
        {
            string json = File.ReadAllText(ConfigPath);
            return JsonConvert.DeserializeObject<RegexConfig>(json);
        }

        private const string ConfigPath = ".\\Config\\RegexConfig.json";
    }
}
