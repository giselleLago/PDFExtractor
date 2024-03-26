using PDFExtractors.Models;
using System.Text.RegularExpressions;

namespace PDFExtractors.Extractor
{
    public class RegexDataExtractorEngine : IDataExtractorEngine
    {
        public RegexDataExtractorEngine(RegexConfig regexConfig)
        {
            RegexConfig = regexConfig;
        }

        public ExtractedPage ExtractDataFromPage(string pageContent)
        {
            var extractedPage = new ExtractedPage();
            RegexConfig.Fields.ForEach(field => ExtractField(pageContent, extractedPage, field));
            return extractedPage;
        }

        private static void ExtractField(string pageContent, ExtractedPage extractedPage, RegexExtractableField field)
        {
            var value = ExecuteRegexChain(pageContent, field.RegexChain, extractedPage);
            if (value == null && field.Required)
            {
                throw new MissingRequiredFieldException($"Unable to extract required field: {field.Id}");
            }
            extractedPage.SetField(field.Id, field.Name, value);
        }

        private static string? ExecuteRegexChain(string? text, IList<string> regexChain, ExtractedPage extractedPage)
        {
            foreach (var regex in regexChain)
            {
                if (text == null)
                    return null;

                var resolvedRegex = ReplaceVariables(regex, extractedPage);
                var match = Regex.Match(text, $"{resolvedRegex}", RegexOptions.IgnoreCase);

                text = match.Success ? match.Groups["output"].Value : null;
            }
            return text;
        }

        private static string ReplaceVariables(string regex, ExtractedPage extractedPage)
        {
            var result = regex;

            foreach (var currentField in extractedPage)
            {
                result = result.Replace("{{" + currentField.Id + "}}", currentField.Value);
            }
            return result;
        }

        private readonly RegexConfig RegexConfig;
    }
}
