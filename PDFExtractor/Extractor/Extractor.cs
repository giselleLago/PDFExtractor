using PDFExtractors.Models;

namespace PDFExtractors.Extractor
{
    public class Extractor
    {
        public Extractor(IPDFReader reader, IDataExtractorEngine dataExtractorEngine)
        {
            Reader = reader;
            DataExtractorEngine = dataExtractorEngine;
        }

        public List<ExtractedPage> ExtractData(string filePath)
        {
            var relevantPagesContent = Reader.GetRelevantPages(filePath);
            return relevantPagesContent.Select(DataExtractorEngine.ExtractDataFromPage).ToList();
        }

        private readonly IPDFReader Reader;
        private readonly IDataExtractorEngine DataExtractorEngine;
    }
}
