using PDFExtractors.Models;

namespace PDFExtractors.Extractor
{
    public class MissingRequiredFieldException : Exception
    {
        public MissingRequiredFieldException(string message) : base(message)
        {
        }

        public MissingRequiredFieldException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
