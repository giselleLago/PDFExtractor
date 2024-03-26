using PDFExtractors.Models;

namespace PDFExtractors.Extractor
{
    public class Extractor(IPDFReader reader, IDataExtractorEngine dataExtractorEngine)
    {
        public List<Dictionary<string, ExtractedField>> ExtractData(string filePath, RegexConfig regexConfig)
        {
            var extractedDataList = new List<Dictionary<string, ExtractedField>>();

            var relevantPages = Reader.GetRelevantPages(filePath);
            foreach (var text in relevantPages)
            {
                var data = DataExtractorEngine.ExtractDataFromPage(text, regexConfig);
                extractedDataList.Add(data);
            }

            return extractedDataList;
        }

        private readonly IPDFReader Reader = reader;
        private readonly IDataExtractorEngine DataExtractorEngine = dataExtractorEngine;
    }
}
