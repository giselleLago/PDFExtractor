namespace PDFExtractors.Models
{
    public sealed record ExtractedField
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public string? Value { get; set; }
    }
}
