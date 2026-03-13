using System.Collections.Generic;
using System.Linq;
using AwesomeAssertions.Formatting;

namespace FluentResults.Extensions.AwesomeAssertions
{
    public class ErrorListValueFormatter : IValueFormatter
    {
        public bool CanHandle(object value)
        {
            return value is not null && value is List<IError>;
        }

        public void Format(object value, FormattedObjectGraph formattedGraph, FormattingContext context, FormatChild formatChild)
        {
            var errors = (IEnumerable<IError>)value;
            formattedGraph.AddFragment(string.Join("; ", errors.Select(error => error.Message)));
        }
    }
}