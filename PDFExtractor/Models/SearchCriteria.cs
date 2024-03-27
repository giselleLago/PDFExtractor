namespace PDFExtractors.Models
{
    public sealed record SearchCriteria
    {
        public required IEnumerable<string> Elements { get; set; }
    }
}

