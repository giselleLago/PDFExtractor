using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PDFExtractors.Extractor
{
    public class PDFReader : IPDFReader
    {
        public List<string> GetRelevantPages(string filePath)
        {
            var relevantPages = new List<string>();

            try
            {
                using var reader = new PdfReader(filePath);
                for (int pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber++)
                {
                    string pageContent = PdfTextExtractor.GetTextFromPage(reader, pageNumber);
                    if (IsPageRelevant(reader, pageContent))
                    {
                        relevantPages.Add(pageContent);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error when extracting data from PDF");
                throw;
            }

            return relevantPages;
        }

        private static bool IsPageRelevant(PdfReader reader, string pageContent)
        {
            return (pageContent.Contains("Flight Info") &&
                    pageContent.Contains("Times") &&
                    pageContent.Contains("Loadmass") &&
                    pageContent.Contains("Fuel") &&
                    pageContent.Contains("CMD Signature") &&
                    pageContent.Contains("Corrections") &&
                    pageContent.Contains("ATC Route") &&
                    pageContent.Contains("ATIS Departure") &&
                    pageContent.Contains("ATIS Destination") &&
                    pageContent.Contains("ATC Clearance") &&
                    pageContent.Contains("T/O Performance") &&
                    pageContent.Contains("Landing Performance") &&

                    pageContent.Contains("Date:") &&
                    pageContent.Contains("Reg.:") &&
                    pageContent.Contains("From:") &&
                    pageContent.Contains("To:")) ;
        }
    }
}
