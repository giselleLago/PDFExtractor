using System.Collections;

namespace PDFExtractors.Models
{
    public sealed class ExtractedPage : IEnumerable<ExtractedField>
    {
        public ExtractedField GetField(string id)
        {
            return Fields[id];
        }

        public void SetField(string id, string name, string? value)
        {
            Fields[id] = new ExtractedField { Id = id, Name = name, Value = value };
        }

        public IEnumerator<ExtractedField> GetEnumerator()
        {
            return Fields.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private readonly Dictionary<string, ExtractedField> Fields = new();
    }
}

