namespace PDFExtractors.Models
{
    public sealed record RegexConfig
    {
        public required List<RegexExtractableField> Fields { get; set; }
    }
}
