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
                    var reader = new PDFReader();
                    var regexExtractor = new RegexDataExtractorEngine();

                    var extractor = new Extractor(reader, regexExtractor);
                    var a = ReadRegexConfig();
                    var extractedData = extractor.ExtractData(pathArg.Value, a);

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
        }

        private static RegexConfig? ReadRegexConfig()
        {
            string json = File.ReadAllText(ConfigPath);
            return JsonConvert.DeserializeObject<RegexConfig>(json);
        }

        private const string ConfigPath = "C:\\Users\\gisel\\source\\repos\\PDFExtractor\\PDFExtractorCLI\\Config\\RegexConfig.json";
    }
}
