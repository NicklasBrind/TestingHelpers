using System;

namespace FluentResults.Extensions.AwesomeAssertions
{
    public static class FluentResultAssertionsConfig
    {
        public static Func<string, string, bool> MessageComparison { get; set; } = MessageComparisonLogics.Equal;
    }
}