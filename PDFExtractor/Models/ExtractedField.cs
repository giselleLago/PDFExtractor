namespace PDFExtractors.Models
{
    public sealed record ExtractedField
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
