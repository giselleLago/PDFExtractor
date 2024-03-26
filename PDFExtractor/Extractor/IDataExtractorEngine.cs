using PDFExtractors.Models;
namespace PDFExtractors.Extractor
{
    public interface IDataExtractorEngine
    {
        Dictionary<string, ExtractedField> ExtractDataFromPage(string text, RegexConfig regexConfig);
    }
}
