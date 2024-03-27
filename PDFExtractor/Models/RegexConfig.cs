namespace PDFExtractors.Models
{
    public sealed record RegexConfig
    {
        public required List<string> RelevantPagesSearchCriteria { get; set; }
        public required List<RegexExtractableField> Fields { get; set; }
    }
}
