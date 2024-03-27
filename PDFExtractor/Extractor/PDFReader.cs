using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using PDFExtractors.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace PDFExtractors.Extractor
{
    public class PDFReader : IPDFReader
    {
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

        private static bool IsPageRelevant(string pageContent)
        {
            string json = File.ReadAllText(DataSearchCriteriaPath);
            var searchCriteria = JsonConvert.DeserializeObject<SearchCriteria>(json);
            if (searchCriteria == null)
            {
                throw new Exception("Unable to read data search criteria file.");
            }

            foreach (var element in searchCriteria.Elements) 
            { 
                if (!pageContent.Contains(element))
                {
                    return false;
                }
            }

            return true;
        }

        private const string DataSearchCriteriaPath = ".\\DataSearchCriteriaConfig.json";
    }
}
