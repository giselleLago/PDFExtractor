using PDFExtractors.Models;
using System.Text.RegularExpressions;

namespace PDFExtractors.Extractor
{
    public class RegexDataExtractorEngine : IDataExtractorEngine
    {
        public Dictionary<string, ExtractedField> ExtractDataFromPage(string text, RegexConfig regexConfig)
        {
            var extractedFieldDict = new Dictionary<string, ExtractedField>();
            foreach (var item in regexConfig.Fields)
            {
                var value = ExecuteRegex(text, item.RegexList, extractedFieldDict);
                if (value == null && item.Required)
                {
                    throw new MissingRequiredFieldException($"Unable to extract required field: {item.Id}");
                }
                var extractedField = new ExtractedField { Id = item.Id, Name = item.Name, Value = value };
                extractedFieldDict.TryAdd(item.Id, extractedField);
            }

            return extractedFieldDict;
        }

        private static string? ExecuteRegex(string? text, IList<string> regexList, Dictionary<string, ExtractedField> extractedFieldDictionary)
        {
            foreach (var regex in regexList)
            {
                if (text == null)
                    return null;

                var resolvedRegex = ResolveRegexVariables(regex, extractedFieldDictionary);
                var match = Regex.Match(text, $"{resolvedRegex}", RegexOptions.IgnoreCase);

                text = match.Success ? match.Groups["output"].Value : null;
            }

            return text;
        }

        private static string ResolveRegexVariables(string regex, Dictionary<string, ExtractedField> extractedFieldDictionary)
        {
            string result = regex;

            foreach (var currentField in extractedFieldDictionary)
            {
                result = result.Replace("{{" + currentField.Value.Id + "}}", currentField.Value.Value);
            }

            return result;
        }
    }
}
