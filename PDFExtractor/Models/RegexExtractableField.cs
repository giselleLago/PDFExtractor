namespace PDFExtractors.Models
{
    public sealed record RegexExtractableField
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public required List<string> RegexList { get; set; }

        public bool Required { get; set; }
    }
}
