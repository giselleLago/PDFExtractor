using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PDFExtractors.Models;

namespace PDFExtractors.Extractor
{
    public class PDFReader : IPDFReader
    {
        public PDFReader(RegexConfig regexConfig)
        {
            RegexConfig = regexConfig;
        }

        public List<string> GetRelevantPages(string filePath)
        {
            var relevantPages = new List<string>();

            using var reader = new PdfReader(filePath);
            for (int pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber++)
            {
                string pageContent = PdfTextExtractor.GetTextFromPage(reader, pageNumber);
                if (IsPageRelevant(pageContent))
                {
                    relevantPages.Add(pageContent);
                }
            }

            return relevantPages;
        }

        private bool IsPageRelevant(string pageContent)
        {
            foreach (var element in RegexConfig.RelevantPagesSearchCriteria)
            {
                if (!pageContent.Contains(element))
                {
                    return false;
                }
            }

            return true;
        }

        private readonly RegexConfig RegexConfig;
    }
}
