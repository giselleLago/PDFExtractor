namespace PDFExtractors.Extractor
{
    public interface IPDFReader
    {
        List<string> GetRelevantPages(string filePath);
    }
}
