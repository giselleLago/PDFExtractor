using Newtonsoft.Json;
using PDFExtractors.Extractor;
using PDFExtractors.Models;

namespace PDFExtractor.Tests
{
    public class ExtractedDataTests
    {
        [Fact]
        public void GetExtractedData_AllRequiredFields_ShouldBeOk()
        {
            var regexConfig = GetRegexConfig(".\\TestData\\RegexConfig.json");
            var engine = new RegexDataExtractorEngine(regexConfig);

            var pageContent = GetPageContent(".\\TestData\\PageContent.txt");
            var extractedPage = engine.ExtractDataFromPage(pageContent);

            Assert.NotNull(extractedPage);
            Assert.Equal(6, extractedPage.Count());
        }

        [Fact]
        public void GetExtractedData_MissingRequiredFields_ExceptionShouldBeCaptured()
        {
            var regexConfig = GetRegexConfig(".\\TestData\\RegexConfigMissingRequiredField.json");
            var engine = new RegexDataExtractorEngine(regexConfig);

            var pageContent = GetPageContent(".\\TestData\\PageContent.txt");

            Assert.Throws<MissingRequiredFieldException>(() => engine.ExtractDataFromPage(pageContent));
        }

        private static string GetPageContent(string path)
        {
            return File.ReadAllText(path);
        }

        private static RegexConfig GetRegexConfig(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<RegexConfig>(json);
        }
    }
}