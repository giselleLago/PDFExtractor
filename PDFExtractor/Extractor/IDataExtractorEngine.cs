using PDFExtractors.Models;
namespace PDFExtractors.Extractor
{
    public interface IDataExtractorEngine
    {
        ExtractedPage ExtractDataFromPage(string pageContent);
    }
}
